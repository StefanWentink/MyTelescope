namespace MyTelescope.Seeder.Helpers
{
    using Core.Utilities.Helpers;
    using MoreLinq;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Utilities.Interfaces;
    using Utilities.Interfaces.Connector;
    using Utilities.Models.Sort;

    public abstract class BaseSeeder<TModel, TKey>
        where TModel : class, IKeyModel<Guid>
    {
        protected BaseSeeder(IContextConnector<TModel> connector)
        {
            Connector = connector;
        }

        private IContextConnector<TModel> Connector { get; }

        private List<TModel> ExistingList(Expression<Func<TModel, bool>> batchExpression)
        {
            return Task.Run(() => Connector.ReadAsync(batchExpression, new SortModel(nameof(IKeyModel<Guid>.Id)))).Result.ToList();
        }

        protected abstract List<Expression<Func<TModel, bool>>> GetBatchExpression();

        protected abstract List<TModel> SeedList(Expression<Func<TModel, bool>> batchExpression);

        protected abstract Func<TModel, TKey> DuplicateCheckFunction { get; }

        private List<TModel> GetCreateList(Expression<Func<TModel, bool>> batchExpression)
        {
            var existingList = ExistingList(batchExpression);
            var seedList = SeedList(batchExpression);

            return seedList.ExceptBy(existingList, DuplicateCheckFunction).ToList();
        }

        private readonly object _resultLock = new object();

        public List<TModel> Seed()
        {
            var result = new List<TModel>();

            try
            {
                Parallel.ForEach(
                    GetBatchExpression(),
                    (batchExpression) =>
                    {
                        var createList = GetCreateList(batchExpression);
                        if (createList.Count > 0)
                        {
                            LogHelper.LogInformation($"Start seeding {createList.Count} {nameof(TModel)}.");
                            var task = Task.Run(() => Connector.CreateAsync(createList).Result);

                            if (!task.Result)
                            {
                                throw new InvalidOperationException("Create task failed.");
                            }

                            var existingList = ExistingList(batchExpression);

                            lock (_resultLock)
                            {
                                result.AddRange(existingList);
                            }
                        }
                    });
            }
            catch (AggregateException exception)
            {
                foreach (var innerExcception in exception.InnerExceptions)
                {
                    LogHelper.LogError(innerExcception);
                }

                return new List<TModel>();
            }

            return result;
        }
    }
}