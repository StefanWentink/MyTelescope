namespace MyTelescope.App.OData.Models.DataLoader
{
    using MyTelescope.Utilities.Interfaces.Connector;
    using MyTelescope.Utilities.Models.Filter;
    using MyTelescope.Utilities.Models.Sort;
    using SolarSystem.Models.CelestialObject;
    using SWE.Http.Interfacess;
    using SWE.OData.Enums;
    using SWE.OData.Interfaces;
    using SWE.OData.Models;
    using System;
    using System.Collections.Generic;
    using ViewModels.Helpers.Filter;
    using ViewModels.Models.Item;

    public class CelestialObjectPositionDataLoader : HttpDataLoader<CelestialObjectPositionViewModel, CelestialObjectPosition>
    {
        public CelestialObjectPositionDataLoader(IRepository repository)
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

        protected override IODataFilters<CelestialObjectPosition> GetModelFilters(CelestialObjectPosition model)
        {
            return model == null
                ? base.GetModelFilters(null)
                : GetReferenceFilterItems(model);
        }

        private IODataFilters<CelestialObjectPosition> GetReferenceFilterItems(CelestialObjectPosition model)
        {
            return model.ReferenceEndDate.HasValue
                ? new ODataFilters<CelestialObjectPosition>(
                    QueryOperator.And,
                    new ODataFilter<CelestialObjectPosition, Guid>(x => x.CelestialObjectId, FilterOperator.Equal, model.CelestialObjectId),
                    new ODataFilters<CelestialObjectPosition>(
                        QueryOperator.And,
                        new ODataFilter<CelestialObjectPosition, DateTimeOffset>(x => x.ReferenceDate, FilterOperator.GreaterOrEquals, model.ReferenceDate),
                        new ODataFilter<CelestialObjectPosition, DateTimeOffset?>(x => x.ReferenceEndDate, FilterOperator.LessOrEquals, model.ReferenceEndDate)))

                : new ODataFilters<CelestialObjectPosition>(
                    QueryOperator.And,
                    new ODataFilter<CelestialObjectPosition, Guid>(x => x.CelestialObjectId, FilterOperator.Equal, model.CelestialObjectId),
                    new ODataFilter<CelestialObjectPosition, DateTimeOffset>(x => x.ReferenceDate, FilterOperator.GreaterOrEquals, model.ReferenceDate));
        }
    }
}