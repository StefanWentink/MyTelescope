namespace MyTelescope.Ef.Utilities.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Core.Utilities.Helpers;
    using Microsoft.EntityFrameworkCore;
    using MyTelescope.Utilities.Helpers.Reflection;
    using MyTelescope.Utilities.Models.Sort;

    public static class QueryHelper
    {
        public static async Task<int> GetCount<T>(this IQueryable<T> query, Expression<Func<T, bool>> exp)
            where T : class
        {
            return await query.ApplyExpression(exp).CountAsync().ConfigureAwait(false);
        }

        public static async Task<T> GetFirst<T>(this IQueryable<T> query, Expression<Func<T, bool>> exp)
            where T : class
        {
            return await query.ApplyExpression(exp).FirstOrDefaultAsync().ConfigureAwait(false);
        }

        public static async Task<List<T>> GetData<T>(this IQueryable<T> query, Expression<Func<T, bool>> exp, SortModel sort)
            where T : class
        {
            var result = query.ApplyExpression(exp);

            return await result.ResolveData(sort).ConfigureAwait(false);
        }

        private static IQueryable<T> ApplyExpression<T>(this IQueryable<T> query, Expression<Func<T, bool>> exp)
            where T : class
        {
            return exp == null ? query : query.Where(exp);
        }

        private static IQueryable<T> ApplySkipTake<T>(this IQueryable<T> query, SortModel sort)
            where T : class
        {
            if (sort.Take <= 0)
            {
                return query;
            }

            return (sort.Skip > 0) ? query.Skip(sort.Skip).Take(sort.Take) : query.Take(sort.Take);
        }

        private static async Task<List<TModel>> ResolveData<TModel>(this IQueryable<TModel> query, SortModel sort)
            where TModel : class
        {
            try
            {
                // With Orderby the function can be resolved with 'ToListAsync'. (Otherwise a IOrderedQueryable is returned)
                if (sort.Take <= 0)
                {
                    var resultTask = query.ToListAsync().ConfigureAwait(false);

                    return await resultTask;
                }
                
                IOrderedQueryable<TModel> orderedQueryable = null;

                foreach (var sortitem in sort.SortItems.Where(x => !string.IsNullOrEmpty(x.Column)))
                {
                    var sortExpression = ReflectionHelper.MemberSelector<TModel>(sortitem.Column);

                    orderedQueryable = orderedQueryable == null
                        ? (sortitem.Ascending ? query.OrderBy(sortExpression) : query.OrderByDescending(sortExpression))
                        : (sortitem.Ascending ? orderedQueryable.ThenBy(sortExpression) : orderedQueryable.ThenByDescending(sortExpression));
                }

                return await (orderedQueryable ?? query).ApplySkipTake(sort).ToListAsync().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                LogHelper.LogError(ex);
                throw;
            }
        }
    }
}
