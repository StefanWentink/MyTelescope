namespace MyTelescope.App.OData.Models.DataLoader
{
    using MyTelescope.Utilities.Models.Sort;
    using SolarSystem.Models.CelestialObject;
    using SWE.Http.Interfaces;
    using SWE.OData.Enums;
    using SWE.OData.Interfaces;
    using SWE.OData.Models;
    using System;
    using System.Collections.Generic;
    using ViewModels.Models.Item;

    public class CelestialObjectPositionDataLoader : HttpDataLoader<CelestialObjectPositionViewModel, CelestialObjectPosition>
    {
        public CelestialObjectPositionDataLoader(IRepository<CelestialObjectPosition> repository)
            : base(repository)
        {
        }

        protected override SortModel GetSort()
        {
            return new SortModel(
                new List<SortItemModel>
                {
                    new SortItemModel(nameof(CelestialObjectPosition.CelestialObjectId), true),
                    new SortItemModel(nameof(CelestialObjectPosition.ReferenceDate), true)
                });
        }

        protected override List<IODataFilter> GetModelFilters(CelestialObjectPosition model)
        {
            return model == null
                ? base.GetModelFilters(null)
                : GetReferenceFilterItems(model);
        }

        private List<IODataFilter> GetReferenceFilterItems(CelestialObjectPosition model)
        {
            var result = new List<IODataFilter>
            {
                new ODataFilterSelector<CelestialObjectPosition, Guid>(x => x.CelestialObjectId, FilterOperator.Equal, model.CelestialObjectId),
                new ODataFilterSelector<CelestialObjectPosition, DateTimeOffset>(x => x.ReferenceDate, FilterOperator.GreaterOrEquals, model.ReferenceDate)
            };

            if (model.ReferenceEndDate.HasValue)
            {
                result.Add(new ODataFilterSelector<CelestialObjectPosition, DateTimeOffset?>(x => x.ReferenceDate, FilterOperator.LessOrEquals, model.ReferenceEndDate));
            }

            return result;
        }
    }
}