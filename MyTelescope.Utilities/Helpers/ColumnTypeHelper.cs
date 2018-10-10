namespace MyTelescope.Utilities.Helpers
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using Enums;

    public static class ColumnTypeHelper
    {
        private static ConcurrentDictionary<ColumnType, List<FilterType>> Dictionary { get; } = new ConcurrentDictionary<ColumnType, List<FilterType>>();

        public static List<FilterType> GetValidFilterTypes(ColumnType type)
        {
            switch (type)
            {
                case ColumnType.StringColumn:
                    return GetValidStringFilterTypes();
                case ColumnType.GuidColumn:
                    return GetValidGuidFilterTypes();
                case ColumnType.BoolColumn:
                    return GetValidBoolFilterTypes();
                case ColumnType.DateTimeOffsetColumn:
                case ColumnType.IntColumn:
                case ColumnType.DoubleColumn:
                    return GetValidCompareFilterTypes(type);
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        private static List<FilterType> GetValidStringFilterTypes()
        {
            return Dictionary.GetOrAdd(ColumnType.BoolColumn, new List<FilterType> { FilterType.Equal, FilterType.NotEqual, FilterType.Contains, FilterType.NotContains, FilterType.In });
        }

        private static List<FilterType> GetValidGuidFilterTypes()
        {
            return Dictionary.GetOrAdd(ColumnType.BoolColumn, new List<FilterType> { FilterType.Equal, FilterType.NotEqual, FilterType.In });
        }

        private static List<FilterType> GetValidBoolFilterTypes()
        {
            return Dictionary.GetOrAdd(ColumnType.BoolColumn, new List<FilterType> { FilterType.Equal });
        }

        private static List<FilterType> GetValidCompareFilterTypes(ColumnType type)
        {
            return Dictionary.GetOrAdd(
                type,
                new List<FilterType>
                {
                    FilterType.Equal,
                    FilterType.NotEqual,
                    FilterType.GreaterOrEqual,
                    FilterType.LessOrEqual,
                    FilterType.In
                });
        }
    }
}
