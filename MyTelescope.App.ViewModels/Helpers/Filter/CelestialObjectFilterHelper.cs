namespace MyTelescope.App.ViewModels.Helpers.Filter
{
    using System;
    using System.Collections.Generic;
    using MyTelescope.Utilities.Enums;
    using MyTelescope.Utilities.Models.Filter;
    using SolarSystem.Enums;
    using SolarSystem.Models.CelestialObject;

    public static class CelestialObjectFilterHelper
    {
        public static List<FilterItemModel> GetDefaultMoonFilterList(CelestialObjectType celestialObjectType, Guid celestialObjectId)
        {
            return new List<FilterItemModel>
            {
                GetCelestialObjectTypeFilter(CelestialObjectType.MajorMoon),
                GetParentCelestialObjectFilter(celestialObjectId)
            };
        }

        public static List<FilterItemModel> GetDefaultFilterList(CelestialObjectType celestialObjectType)
        {
            return new List<FilterItemModel>
            {
                GetCelestialObjectTypeFilter(celestialObjectType)
            };
        }

        public static FilterItemModel GetCelestialObjectTypeFilter(CelestialObjectType celestialObjectType)
        {
            return new FilterItemModel(
                $"{nameof(CelestialObjectModel.CelestialObjectType)}",
                ColumnType.IntColumn,
                FilterType.Equal,
                (int) celestialObjectType);
        }

        public static FilterItemModel GetParentCelestialObjectFilter(Guid celestialObjectId)
        {
            return new FilterItemModel(
                $"{nameof(CelestialObjectModel.CelestialObjectId)}",
                ColumnType.GuidColumn,
                FilterType.Equal,
                celestialObjectId);
        }
    }
}
