namespace MyTelescope.Api.DataLayer.Helpers
{
    using Factories;
    using SolarSystem.Enums;
    using System;
    using System.Linq.Expressions;
    using Utilities.Enums;
    using Utilities.Helpers;
    using Utilities.Interfaces;
    using Utilities.Models.Filter;

    public static class CustomFilterItemHelper
    {
        public static Expression<Func<TModel, bool>> GetCelestialObjectTypeExpression<TModel>(FilterModel filter)
        where TModel : class, ICelestialObjectTypeReferenceModel
        {
            for (var index = filter.FilterItems.Count - 1; index >= 0; index--)
            {
                var filterItem = filter.FilterItems[index];

                if (filterItem.Column.Equals(nameof(CelestialObjectType)))
                {
                    if (filterItem.Filter != FilterType.Equal)
                    {
                        throw new ArgumentException($"{nameof(CelestialObjectType)}-filter only supports {nameof(FilterType)} {FilterType.Equal}.");
                    }

                    if (IntHelper.IntIsNullOrEmpty(filterItem.Value))
                    {
                        throw new ArgumentException($"{filterItem.Value} is not a valid value.");
                    }

                    var celestialObjectType = (CelestialObjectType)IntHelper.ToInt(filterItem.Value);

                    var model = CelestialObjectTypeFactory.Instance.GetSingleByEnum(celestialObjectType);

                    filter.FilterItems.RemoveAt(index);

                    return x => x.CelestialObjectTypeId == model.Id;
                }
            }

            return null;
        }
    }
}