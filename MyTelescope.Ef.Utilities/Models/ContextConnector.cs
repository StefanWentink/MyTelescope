namespace MyTelescope.Ef.Utilities.Models
{
    using Core.Utilities.Helpers;
    using EFCore.BulkExtensions;
    using Helpers;
    using Interfaces;
    using Microsoft.EntityFrameworkCore;
    using MyTelescope.Utilities.Enums;
    using MyTelescope.Utilities.Helpers.Filter;
    using MyTelescope.Utilities.Helpers.Reflection;
    using MyTelescope.Utilities.Interfaces.Connector;
    using MyTelescope.Utilities.Models.Connector;
    using MyTelescope.Utilities.Models.Filter;
    using MyTelescope.Utilities.Models.Sort;
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;

    public abstract class ContextConnector<TModel> : Connector<TModel>, IContextConnector<TModel>
        where TModel : class
    {
        protected IContextContainer ContextContainer { get; }

        public int RecordCount { get; protected set; }

        public List<string> IncludeList { get; set; }

        protected bool UseBulk { get; }

        protected ContextConnector(IContextContainer contextContainer)
            : this(contextContainer, false)
        {
        }

        protected ContextConnector(IContextContainer contextContainer, bool useBulk)
        {
            ContextContainer = contextContainer;
            UseBulk = useBulk;
        }

        protected abstract Expression<Func<TModel, bool>> GetCustomExpression(FilterModel filter);

        protected override async Task<List<TModel>> Get(FilterModel filter)
        {
            var customExpression = GetCustomExpression(filter);
            var expression = FilterHelper.ToExpression<TModel>(filter);
            return await ReadAsync(customExpression.CombineExpressionAnd(expression), filter.Sort).ConfigureAwait(false);
        }

        protected override async Task<bool> ProcessAsync(List<TModel> models, ProcessDirective directive)
        {
            using (var context = ContextContainer.GetContext)
            {
                try
                {
                    if (UseBulk)
                    {
                        switch (directive)
                        {
                            case ProcessDirective.Create:
                                await context.BulkInsertAsync(
                                    models,
                                    new BulkConfig { PreserveInsertOrder = true, SetOutputIdentity = true, BatchSize = 4000 }).ConfigureAwait(false);
                                return true;

                            case ProcessDirective.Update:
                                await context.BulkUpdateAsync(
                                    models,
                                    new BulkConfig { PreserveInsertOrder = true, SetOutputIdentity = true, BatchSize = 4000 }).ConfigureAwait(false);
                                return true;

                            case ProcessDirective.Delete:
                                await context.BulkDeleteAsync(
                                    models,
                                    new BulkConfig { PreserveInsertOrder = true, SetOutputIdentity = true, BatchSize = 4000 }).ConfigureAwait(false);
                                return true;

                            default:
                                throw new ArgumentOutOfRangeException(nameof(directive), directive, null);
                        }
                    }

                    var process = 0;
                    const int step = 1000;
                    var count = models.Count;
                    while (process < models.Count)
                    {
                        var take = step;
                        if (process + step >= count)
                        {
                            take = (process + step) - count + 1;
                        }

                        var processModels = models.Skip(process).Take(take).ToList();
                        switch (directive)
                        {
                            case ProcessDirective.Create:
                                await context.GetObjectSet<TModel>().AddRangeAsync(processModels).ConfigureAwait(false);
                                break;

                            case ProcessDirective.Update:
                                context.GetObjectSet<TModel>().UpdateRange(processModels);
                                break;

                            case ProcessDirective.Delete:
                                context.GetObjectSet<TModel>().RemoveRange(processModels);
                                break;

                            default:
                                throw new ArgumentOutOfRangeException(nameof(directive), directive, null);
                        }

                        process += step;

                        try
                        {
                            await context.SaveChangesAsync().ConfigureAwait(false);
                        }
                        catch (DbUpdateException exception)
                        {
                            LogHelper.LogError(exception);
                            return false;
                        }
                        catch (Exception exception)
                        {
                            LogHelper.LogError(exception);
                            return false;
                        }
                    }

                    return true;
                }
                catch (Exception exception)
                {
                    LogHelper.LogError(exception);
                    return false;
                }
            }
        }

        public async Task<List<TModel>> ReadAsync(Expression<Func<TModel, bool>> expression, SortModel sort)
        {
            try
            {
                var combinedExpression = CombineExpression(expression);

                // Kickoff async RecordCount, transaction on another SqlTransaction.
                var countTask =
                    sort.Skip == 0
                        ? Return().ConfigureAwait(false)
                        : ProcessRecordCount(combinedExpression).ConfigureAwait(false);

                using (var context = ContextContainer.GetContext)
                {
                    var databaseQuery = context.GetNoTrackingQuery<TModel>(IncludeList);

                    try
                    {
                        context.OpenConnection();

                        return await databaseQuery.GetData(combinedExpression, sort).ConfigureAwait(false);
                    }
                    catch (Exception ex)
                    {
                        LogHelper.LogError(ex);

                        return await RetryRead(countTask, databaseQuery, combinedExpression, sort).ConfigureAwait(false);
                    }
                    finally
                    {
                        context.CloseConnection();
                    }
                }
            }
            catch (InvalidOperationException ex)
            {
                ex.ValidateIncludeNotFoundException<TModel>();
                LogHelper.LogError(ex);
                throw;
            }
            catch (SqlException ex)
            {
                LogHelper.LogError(ex);
                throw;
            }
            catch (Exception ex)
            {
                LogHelper.LogError(ex);
                throw;
            }
        }

        public Expression<Func<TModel, bool>> CombineExpression(Expression<Func<TModel, bool>> expression)
        {
            return expression;
        }

        private static async Task<int> Return()
        {
            return await Task.Run(() => -1).ConfigureAwait(false);
        }

        private async Task<int> ProcessRecordCount(Expression<Func<TModel, bool>> exp)
        {
            using (var context = ContextContainer.GetContext)
            {
                try
                {
                    context.OpenConnection();
                    RecordCount = exp == null
                        ? await context.GetNoTrackingQuery<TModel>().CountAsync().ConfigureAwait(false)
                        : await context.GetNoTrackingQuery<TModel>().Where(exp).CountAsync().ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    LogHelper.LogError(ex);
                    RecordCount = 1;
                }
                finally
                {
                    context.CloseConnection();
                }

                return RecordCount;
            }
        }

        private async Task<List<TModel>> RetryRead(ConfiguredTaskAwaitable<int> countTask, IQueryable<TModel> query, Expression<Func<TModel, bool>> expression, SortModel sort)
        {
            var count = await countTask;

            if (count <= 0)
            {
                // Meaning skip was 0. There are no results.
                return new List<TModel>();
            }

            if (RecordCount < (sort.Skip + sort.Take))
            {
                if (RecordCount < sort.Skip)
                {
                    LogHelper.LogDebug($"Skip exceeded: skip({sort.Skip}), recordcount({RecordCount}).");

                    return new List<TModel>();
                }

                LogHelper.LogDebug($"Skip take exceeded: skip({sort.Skip}), take({sort.Take}), recordcount({RecordCount}).");
                sort.Take = Math.Min(sort.Take, RecordCount - sort.Skip + sort.Take);
                LogHelper.LogDebug($"Retry with sort {sort.Skip} take {sort.Take}");

                return await query.GetData(expression, sort).ConfigureAwait(false);
            }

            LogHelper.LogError($"Something went wrong fetching the data");
            return new List<TModel>();
        }
    }
}