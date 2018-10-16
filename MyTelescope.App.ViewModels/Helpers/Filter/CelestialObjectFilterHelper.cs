namespace MyTelescope.App.ViewModels.Helpers.Filter
{
    using MyTelescope.Utilities.Enums;
    using MyTelescope.Utilities.Models.Filter;
    using SolarSystem.Enums;
    using SolarSystem.Models.CelestialObject;
    using System;
    using System.Collections.Generic;

    public static class CelestialObjectFilterHelper
    {
        public static List<FilterItemModel> GetDefaultMoonFilterList(CelestialType celestialObjectType, Guid celestialObjectId)
        {
            return new List<FilterItemModel>
            {
                GetCelestialObjectTypeFilter(CelestialType.MajorMoon),
                GetParentCelestialObjectFilter(celestialObjectId)
            };
        }

        public static List<FilterItemModel> GetDefaultFilterList(CelestialType celestialObjectType)
        {
            return new List<FilterItemModel>
            {
                GetCelestialObjectTypeFilter(celestialObjectType)
            };
        }

        public static FilterItemModel GetCelestialObjectTypeFilter(CelestialType celestialObjectType)
        {
            return new FilterItemModel(
                nameof(CelestialObject.CelestialObjectType),
                ColumnType.IntColumn,
                FilterType.Equal,
                (int)celestialObjectType);
        }

        public static FilterItemModel GetParentCelestialObjectFilter(Guid celestialObjectId)
        {
            return new FilterItemModel(
                nameof(CelestialObject.CelestialObjectId),
                ColumnType.GuidColumn,
                FilterType.Equal,
                celestialObjectId);
        }
    }
}