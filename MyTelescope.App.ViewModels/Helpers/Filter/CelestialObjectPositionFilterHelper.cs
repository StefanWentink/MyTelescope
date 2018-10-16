namespace MyTelescope.App.ViewModels.Helpers.Filter
{
    using MyTelescope.Utilities.Enums;
    using MyTelescope.Utilities.Models.Filter;
    using SolarSystem.Models.CelestialObject;
    using System;
    using System.Collections.Generic;

    public static class CelestialObjectPositionFilterHelper
    {
        public static List<FilterItemModel> GetDefaultFilterList(Guid parentCelestialObjectId)
        {
            return new List<FilterItemModel>
            {
                GetParentCelestialObjectIdFilter(parentCelestialObjectId)
            };
        }

        public static List<FilterItemModel> GetDefaultFilterList(Guid parentCelestialObjectId, DateTimeOffset referenceDate)
        {
            return new List<FilterItemModel>
            {
                GetParentCelestialObjectIdFilter(parentCelestialObjectId),
                GetReferenceDateFilter(referenceDate)
            };
        }

        public static List<FilterItemModel> GetDefaultFilterList(Guid parentCelestialObjectId, DateTimeOffset intervalStart, DateTimeOffset intervalEnd)
        {
            return new List<FilterItemModel>
            {
                GetParentCelestialObjectIdFilter(parentCelestialObjectId),
                GetReferenceDateGreaterOrEqualFilter(intervalStart),
                GetReferenceDateLessOrEqualFilter(intervalEnd)
            };
        }

        public static FilterItemModel GetParentCelestialObjectIdFilter(Guid parentCelestialObjectId)
        {
            return new FilterItemModel(
                $"{nameof(CelestialObjectPosition.CelestialObject)}.{nameof(CelestialObject.CelestialObjectId)}",
                ColumnType.GuidColumn,
                FilterType.Equal,
                parentCelestialObjectId);
        }

        public static FilterItemModel GetReferenceDateFilter(DateTimeOffset referenceDate)
        {
            return new FilterItemModel(
                nameof(CelestialObjectPosition.ReferenceDate),
                ColumnType.DateTimeOffsetColumn,
                FilterType.Equal,
                referenceDate.GetUtcReferenceDate());
        }

        public static FilterItemModel GetReferenceDateGreaterOrEqualFilter(DateTimeOffset referenceDate)
        {
            return new FilterItemModel(
                nameof(CelestialObjectPosition.ReferenceDate),
                ColumnType.DateTimeOffsetColumn,
                FilterType.GreaterOrEqual,
                referenceDate.GetUtcReferenceDate());
        }

        public static FilterItemModel GetReferenceDateLessOrEqualFilter(DateTimeOffset referenceDate)
        {
            return new FilterItemModel(
                nameof(CelestialObjectPosition.ReferenceDate),
                ColumnType.DateTimeOffsetColumn,
                FilterType.LessOrEqual,
                referenceDate.GetUtcReferenceDate());
        }

        private static DateTimeOffset GetUtcReferenceDate(this DateTimeOffset referenceDate)
        {
            return new DateTimeOffset(new DateTime(referenceDate.Year, referenceDate.Month, referenceDate.Day, 0, 0, 0), TimeSpan.Zero);
        }
    }
}