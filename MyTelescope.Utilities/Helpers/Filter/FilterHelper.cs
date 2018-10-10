namespace MyTelescope.Utilities.Helpers.Filter
{
    using System;
    using System.Linq.Expressions;
    using Models.Filter;
    using Reflection;

    public static class FilterHelper
    {
        public static Expression<Func<TModel, bool>> ToExpression<TModel>(FilterModel filter)
            where TModel : class
        {
            Expression<Func<TModel, bool>> result = null;
            foreach (var filterItem in filter.FilterItems)
            {
                result = result.CombineExpressionAnd(FilterItemHelper.ToExpression<TModel>(filterItem));
            }

            return result;
        }
    }
}
