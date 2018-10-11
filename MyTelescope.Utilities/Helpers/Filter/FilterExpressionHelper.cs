namespace MyTelescope.Utilities.Helpers.Filter
{
    using Enums;
    using SWE.BasicType.Utilities;
    using System;
    using System.Linq.Expressions;

    public static class FilterExpressionHelper
    {
        public static Expression<Func<string, bool>> GetExpression(FilterType filter, string value)
        {
            switch (filter)
            {
                case FilterType.Equal:
                    return x => x == value;

                case FilterType.NotEqual:
                    return x => x != value;

                case FilterType.Contains:
                    return x => x.Contains(value);

                case FilterType.NotContains:
                    return x => !x.Contains(value);

                default:
                    throw new ArgumentOutOfRangeException(nameof(filter), filter, $"{filter} is invalid for type {typeof(string)}.");
            }
        }

        public static Expression<Func<int, bool>> GetExpression(FilterType filter, int value)
        {
            switch (filter)
            {
                case FilterType.Equal:
                    return x => x == value;

                case FilterType.NotEqual:
                    return x => x != value;

                case FilterType.GreaterOrEqual:
                    return x => x >= value;

                case FilterType.LessOrEqual:
                    return x => x <= value;

                default:
                    throw new ArgumentOutOfRangeException(nameof(filter), filter, $"{filter} is invalid for type {typeof(int)}.");
            }
        }

        public static Expression<Func<double, bool>> GetExpression(FilterType filter, double value)
        {
            switch (filter)
            {
                case FilterType.Equal:
                    return x => CompareUtilities.EqualsWithinTolerance(x, value, 10);

                case FilterType.NotEqual:
                    return x => !CompareUtilities.EqualsWithinTolerance(x, value, 10);

                case FilterType.GreaterOrEqual:
                    return x => x >= value;

                case FilterType.LessOrEqual:
                    return x => x <= value;

                default:
                    throw new ArgumentOutOfRangeException(nameof(filter), filter, $"{filter} is invalid for type {typeof(int)}.");
            }
        }

        public static Expression<Func<DateTimeOffset, bool>> GetExpression(FilterType filter, DateTimeOffset value)
        {
            switch (filter)
            {
                case FilterType.Equal:
                    return x => x == value;

                case FilterType.NotEqual:
                    return x => x != value;

                case FilterType.GreaterOrEqual:
                    return x => x >= value;

                case FilterType.LessOrEqual:
                    return x => x <= value;

                default:
                    throw new ArgumentOutOfRangeException(nameof(filter), filter, $"{filter} is invalid for type {typeof(int)}.");
            }
        }

        public static Expression<Func<Guid, bool>> GetExpression(FilterType filter, Guid value)
        {
            switch (filter)
            {
                case FilterType.Equal:
                    return x => x == value;

                case FilterType.NotEqual:
                    return x => x != value;

                default:
                    throw new ArgumentOutOfRangeException(nameof(filter), filter, null);
            }
        }

        public static Expression<Func<bool, bool>> GetExpression(FilterType filter, bool value)
        {
            switch (filter)
            {
                case FilterType.Equal:
                    return x => x == value;

                case FilterType.NotEqual:
                    return x => x != value;

                default:
                    throw new ArgumentOutOfRangeException(nameof(filter), filter, null);
            }
        }

        public static Expression<Func<int?, bool>> GetNullableExpression(FilterType filter, int value)
        {
            switch (filter)
            {
                case FilterType.Equal:
                    return x => x.HasValue && x.Value == value;

                case FilterType.NotEqual:
                    return x => !x.HasValue || x.Value != value;

                case FilterType.GreaterOrEqual:
                    return x => x.HasValue && x.Value >= value;

                case FilterType.LessOrEqual:
                    return x => x.HasValue && x.Value <= value;

                default:
                    throw new ArgumentOutOfRangeException(nameof(filter), filter, $"{filter} is invalid for type {typeof(int)}.");
            }
        }

        public static Expression<Func<double?, bool>> GetNullableExpression(FilterType filter, double value)
        {
            switch (filter)
            {
                case FilterType.Equal:
                    return x => x.HasValue && CompareUtilities.EqualsWithinTolerance(x.Value, value, 10);

                case FilterType.NotEqual:
                    return x => !x.HasValue || !CompareUtilities.EqualsWithinTolerance(x.Value, value, 10);

                case FilterType.GreaterOrEqual:
                    return x => x.HasValue && x.Value >= value;

                case FilterType.LessOrEqual:
                    return x => x.HasValue && x.Value <= value;

                default:
                    throw new ArgumentOutOfRangeException(nameof(filter), filter, $"{filter} is invalid for type {typeof(int)}.");
            }
        }

        public static Expression<Func<DateTimeOffset?, bool>> GetNullableExpression(FilterType filter, DateTimeOffset value)
        {
            switch (filter)
            {
                case FilterType.Equal:
                    return x => x.HasValue && x.Value == value;

                case FilterType.NotEqual:
                    return x => !x.HasValue || x.Value != value;

                case FilterType.GreaterOrEqual:
                    return x => x.HasValue && x.Value >= value;

                case FilterType.LessOrEqual:
                    return x => x.HasValue && x.Value <= value;

                default:
                    throw new ArgumentOutOfRangeException(nameof(filter), filter, $"{filter} is invalid for type {typeof(DateTimeOffset)}.");
            }
        }

        public static Expression<Func<Guid?, bool>> GetNullableExpression(FilterType filter, Guid value)
        {
            switch (filter)
            {
                case FilterType.Equal:
                    return x => x.HasValue && x.Value == value;

                case FilterType.NotEqual:
                    return x => !x.HasValue || x.Value != value;

                default:
                    throw new ArgumentOutOfRangeException(nameof(filter), filter, null);
            }
        }

        public static Expression<Func<bool?, bool>> GetNullableExpression(FilterType filter, bool value)
        {
            switch (filter)
            {
                case FilterType.Equal:
                    return x => x.HasValue && x.Value == value;

                case FilterType.NotEqual:
                    return x => !x.HasValue || x.Value != value;

                default:
                    throw new ArgumentOutOfRangeException(nameof(filter), filter, null);
            }
        }
    }
}