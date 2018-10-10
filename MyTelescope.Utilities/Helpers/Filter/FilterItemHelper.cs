namespace MyTelescope.Utilities.Helpers.Filter
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Enums;
    using Models.Filter;
    using Reflection;

    public static class FilterItemHelper
    {
        public static Expression<Func<TModel, bool>> ToExpression<TModel>(FilterItemModel filterItem)
            where TModel : class
        {
            var isNullable = ReflectionHelper.IsNullable<TModel>(filterItem.Column);
            return ToSingleValueExpression<TModel>(filterItem, isNullable);
        }

        private static Expression<Func<TModel, bool>> ToSingleValueExpression<TModel>(FilterItemModel filterItem, bool isNullable)
        where TModel : class
        {
            switch (filterItem.Type)
            {
                case ColumnType.StringColumn:
                    return ToStringValueExpression<TModel>(filterItem);
                case ColumnType.GuidColumn:
                    return ToGuidValueExpression<TModel>(filterItem, isNullable);
                case ColumnType.BoolColumn:
                    return ToBoolValueExpression<TModel>(filterItem, isNullable);
                case ColumnType.DateTimeOffsetColumn:
                    return ToDateTimeOffsetValueExpression<TModel>(filterItem, isNullable);
                case ColumnType.IntColumn:
                    return ToIntValueExpression<TModel>(filterItem, isNullable);
                case ColumnType.DoubleColumn:
                    return ToDoubleValueExpression<TModel>(filterItem, isNullable);
                default:
                    throw new ArgumentOutOfRangeException($"{nameof(ColumnType)} {filterItem.Type} is not supported.", nameof(filterItem));
            }
        }

        /// <summary>
        /// Get expression for <see cref="FilterType.In"/> filter
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="filterItem"></param>
        /// <param name="paramSelector"></param>
        /// <param name="datatypeFunc"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">If no collection available</exception>
        public static Expression<Func<TModel, bool>> CollectionExpression<TModel, TValue>(
            this FilterItemModel filterItem,
            Expression<Func<TModel, TValue>> paramSelector,
            Func<object, TValue> datatypeFunc)
            where TModel : class
        {
            if (!CollectionHelper.IsEnumerableObject(filterItem.Value))
            {
                throw new ArgumentException("'In' filter needs a collection as a value.");
            }

            var collection = CollectionHelper.EnumerableObjectToList(filterItem.Value, datatypeFunc);

            if (!collection.Any())
            {
                throw new ArgumentException("'In' filter needs a collection as a value.");
            }

            if (collection.Count == 1)
            {
                filterItem.Value = collection[0];
                filterItem.Filter = FilterType.Equal;

                return null;
            }

            var datatypeParamSelector = CollectionHelper.GetCollectionFilterExpression(collection);
            return paramSelector.CombineSelectorParamExpression(datatypeParamSelector);
        }

        private static Expression<Func<TModel, bool>> ToStringValueExpression<TModel>(FilterItemModel filterItem)
            where TModel : class
        {
            if (string.IsNullOrWhiteSpace(filterItem.Value.ToString()))
            {
                throw new ArgumentException($"{filterItem.Value} is not of type {typeof(string)}.");
            }

            var filterValue = filterItem.Value.ToString();

            var entityParamSelector = ReflectionHelper.MemberSelector<TModel, string>(filterItem.Column);

            if (filterItem.Filter == FilterType.In)
            {
                var result = filterItem.CollectionExpression(entityParamSelector, x => x.ToString());
                if (result != null)
                {
                    return result;
                }
            }

            var expression = FilterExpressionHelper.GetExpression(filterItem.Filter, filterValue);
            return entityParamSelector.CombineSelectorParamExpression(expression);
        }

        private static Expression<Func<TModel, bool>> ToBoolValueExpression<TModel>(FilterItemModel filterItem, bool isNullable)
            where TModel : class
        {
            var nullableFilterValue = BoolHelper.ToBoolOrNull(filterItem.Value);

            if (!nullableFilterValue.HasValue)
            {
                throw new ArgumentException($"{filterItem.Value} is not of type {typeof(bool)}.");
            }

            var filterValue = nullableFilterValue.Value;

            if (isNullable)
            {
                var entityParamSelector = ReflectionHelper.MemberSelector<TModel, bool?>(filterItem.Column);
                var expression = FilterExpressionHelper.GetNullableExpression(filterItem.Filter, filterValue);

                return entityParamSelector.CombineSelectorParamExpression(expression);
            }
            else
            {
                var entityParamSelector = ReflectionHelper.MemberSelector<TModel, bool>(filterItem.Column);
                var expression = FilterExpressionHelper.GetExpression(filterItem.Filter, filterValue);

                return entityParamSelector.CombineSelectorParamExpression(expression);
            }
        }

        private static Expression<Func<TModel, bool>> ToGuidValueExpression<TModel>(FilterItemModel filterItem, bool isNullable)
        where TModel : class
        {
            var nullableFilterValue = GuidHelper.ToGuidOrNull(filterItem.Value);

            if (!nullableFilterValue.HasValue || GuidHelper.GuidIsNullOrEmpty(nullableFilterValue))
            {
                throw new ArgumentException($"{filterItem.Value} is not of type {typeof(Guid)}.");
            }

            var filterValue = nullableFilterValue.Value;

            if (isNullable)
            {
                var entityParamSelector = ReflectionHelper.MemberSelector<TModel, Guid?>(filterItem.Column);

                if (filterItem.Filter == FilterType.In)
                {
                    var result = filterItem.CollectionExpression(entityParamSelector, GuidHelper.ToGuidOrNull);
                    if (result != null)
                    {
                        return result;
                    }
                }

                var expression = FilterExpressionHelper.GetNullableExpression(filterItem.Filter, filterValue);

                return entityParamSelector.CombineSelectorParamExpression(expression);
            }
            else
            {
                var entityParamSelector = ReflectionHelper.MemberSelector<TModel, Guid>(filterItem.Column);

                if (filterItem.Filter == FilterType.In)
                {
                    var result = filterItem.CollectionExpression(entityParamSelector, GuidHelper.ToGuid);
                    if (result != null)
                    {
                        return result;
                    }
                }

                var expression = FilterExpressionHelper.GetExpression(filterItem.Filter, filterValue);

                return entityParamSelector.CombineSelectorParamExpression(expression);
            }
        }

        private static Expression<Func<TModel, bool>> ToIntValueExpression<TModel>(FilterItemModel filterItem, bool isNullable)
            where TModel : class
        {
            var type = ReflectionHelper.GetPropertyType<TModel>(filterItem.Column);
            if (type.IsEnum || type.IsNullableEnum())
            {
                if (filterItem.Filter == FilterType.Equal)
                {
                    return EnumToExpression<TModel>(filterItem, type);
                }

                throw new ArgumentException($"Filter {filterItem.Filter} not supported for type Enum. Only {FilterType.Equal} kan be evaluated.", nameof(filterItem));
            }

            var nullableFilterValue = IntHelper.ToIntOrNull(filterItem.Value);

            if (!nullableFilterValue.HasValue)
            {
                throw new ArgumentException($"{filterItem.Value} is not of type {typeof(int)}.");
            }

            var filterValue = nullableFilterValue.Value;

            if (isNullable)
            {
                var entityParamSelector = ReflectionHelper.MemberSelector<TModel, int?>(filterItem.Column);

                if (filterItem.Filter == FilterType.In)
                {
                    var result = filterItem.CollectionExpression(entityParamSelector, IntHelper.ToIntOrNull);
                    if (result != null)
                    {
                        return result;
                    }
                }

                var expression = FilterExpressionHelper.GetNullableExpression(filterItem.Filter, filterValue);

                return entityParamSelector.CombineSelectorParamExpression(expression);
            }
            else
            {
                var entityParamSelector = ReflectionHelper.MemberSelector<TModel, int>(filterItem.Column);

                if (filterItem.Filter == FilterType.In)
                {
                    var result = filterItem.CollectionExpression(entityParamSelector, IntHelper.ToInt);
                    if (result != null)
                    {
                        return result;
                    }
                }

                var expression = FilterExpressionHelper.GetExpression(filterItem.Filter, filterValue);

                return entityParamSelector.CombineSelectorParamExpression(expression);
            }
        }

        private static Expression<Func<TModel, bool>> ToDoubleValueExpression<TModel>(FilterItemModel filterItem, bool isNullable)
            where TModel : class
        {
            var nullableFilterValue = DoubleHelper.ToDoubleOrNull(filterItem.Value);

            if (!nullableFilterValue.HasValue)
            {
                throw new ArgumentException($"{filterItem.Value} is not of type {typeof(double)}.");
            }

            var filterValue = nullableFilterValue.Value;

            if (isNullable)
            {
                var entityParamSelector = ReflectionHelper.MemberSelector<TModel, double?>(filterItem.Column);
                if (filterItem.Filter == FilterType.In)
                {
                    var result = filterItem.CollectionExpression(entityParamSelector, DoubleHelper.ToDoubleOrNull);
                    if (result != null)
                    {
                        return result;
                    }
                }

                var expression = FilterExpressionHelper.GetNullableExpression(filterItem.Filter, filterValue);

                return entityParamSelector.CombineSelectorParamExpression(expression);
            }
            else
            {
                var entityParamSelector = ReflectionHelper.MemberSelector<TModel, double>(filterItem.Column);
                if (filterItem.Filter == FilterType.In)
                {
                    var result = filterItem.CollectionExpression(entityParamSelector, DoubleHelper.ToDouble);
                    if (result != null)
                    {
                        return result;
                    }
                }

                var expression = FilterExpressionHelper.GetExpression(filterItem.Filter, filterValue);

                return entityParamSelector.CombineSelectorParamExpression(expression);
            }
        }

        private static Expression<Func<TModel, bool>> ToDateTimeOffsetValueExpression<TModel>(FilterItemModel filterItem, bool isNullable)
            where TModel : class
        {
            var nullableFilterValue = DateTimeOffsetHelper.ToDateTimeOffsetOrNull(filterItem.Value);

            if (!nullableFilterValue.HasValue || DateTimeOffsetHelper.DateTimeOffsetIsNullOrEmpty(nullableFilterValue))
            {
                throw new ArgumentException($"{filterItem.Value} is not of type {typeof(DateTimeOffset)}.");
            }

            var filterValue = nullableFilterValue.Value;

            if (isNullable)
            {
                var entityParamSelector = ReflectionHelper.MemberSelector<TModel, DateTimeOffset?>(filterItem.Column);
                if (filterItem.Filter == FilterType.In)
                {
                    var result = filterItem.CollectionExpression(entityParamSelector, DateTimeOffsetHelper.ToDateTimeOffsetOrNull);
                    if (result != null)
                    {
                        return result;
                    }
                }

                var expression = FilterExpressionHelper.GetNullableExpression(filterItem.Filter, filterValue);

                return entityParamSelector.CombineSelectorParamExpression(expression);
            }
            else
            {
                var entityParamSelector = ReflectionHelper.MemberSelector<TModel, DateTimeOffset>(filterItem.Column);
                if (filterItem.Filter == FilterType.In)
                {
                    var result = filterItem.CollectionExpression(entityParamSelector, DateTimeOffsetHelper.ToDateTimeOffset);
                    if (result != null)
                    {
                        return result;
                    }
                }

                var expression = FilterExpressionHelper.GetExpression(filterItem.Filter, filterValue);

                return entityParamSelector.CombineSelectorParamExpression(expression);
            }
        }

        public static Expression<Func<TModel, bool>> EnumToExpression<TModel>(this FilterItemModel filterItem, Type typeEnum)
            where TModel : class
        {
            var isNullable = ReflectionHelper.IsNullable<TModel>(filterItem.Column);
            var propertyType = isNullable ? typeof(int?) : typeof(int);
            var paramExpression = Expression.Parameter(typeof(TModel));
            var propertySelector = filterItem.Column.Split('.').Aggregate<string, Expression>(paramExpression, Expression.Property);
            var castedPropertySelector = Expression.Convert(propertySelector, propertyType);
            var compareExpression = Expression.Constant(IntHelper.ToIntOrNull(filterItem.Value));
            var convertedCompareExpression = Expression.Convert(compareExpression, propertyType);
            var bodyExpression = Expression.Equal(castedPropertySelector, convertedCompareExpression);
            var expression = Expression.Lambda<Func<TModel, bool>>(bodyExpression, paramExpression);

            return expression;
        }
    }
}
