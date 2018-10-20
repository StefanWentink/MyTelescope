namespace MyTelescope.App.OData.Models.DataLoader
{
    using MyTelescope.App.Utilities.Interfaces;
    using MyTelescope.App.ViewModels.Helpers.Filter;
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
        public CelestialObjectPositionDataLoader(IBatchContainer batchContainer, IRepository<CelestialObjectPosition> repository)
            : base(batchContainer, repository)
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
                new ODataFilterSelector<CelestialObjectPosition, Guid>(x => x.CelestialObjectId, FilterOperator.Equal, model.CelestialObjectId, typeof(CelestialObject).Name)
            };

            var referenceDate = model.ReferenceDate;

            var referenceDateFilter = FilterOperator.Equal;

            if (model.ReferenceEndDate.HasValue)
            {
                referenceDateFilter = FilterOperator.GreaterOrEquals;
                var referenceEndDateFilter = FilterOperator.LessThan;

                var referenceEndDate = model.ReferenceEndDate.Value;

                if (referenceEndDate < referenceDate)
                {
                    referenceDateFilter = FilterOperator.LessThan;
                    referenceEndDateFilter = FilterOperator.GreaterOrEquals;
                }

                result.Add(new ODataFilterSelector<CelestialObjectPosition, DateTimeOffset>(x => x.ReferenceDate, referenceEndDateFilter, referenceEndDate));
            }

            result.Add(new ODataFilterSelector<CelestialObjectPosition, DateTimeOffset>(x => x.ReferenceDate, referenceDateFilter, referenceDate));

            return result;
        }
    }
}