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

    public class CelestialObjectDataLoader : HttpDataLoader<CelestialObjectViewModel, CelestialObject>
    {
        public CelestialObjectDataLoader(IRepository repository)
            : base(repository)
        {
        }

        protected override SortModel GetSort()
        {
            return new SortModel(nameof(CelestialObject.SemiMajorAxis), true);
        }

        protected override IODataFilters<CelestialObject> GetModelFilters(CelestialObject model)
        {
            return model == null
                ? base.GetModelFilters(null)
                : new ODataFilters<CelestialObject>(new ODataFilter<CelestialObject, Guid?>(x => x.CelestialObjectId, FilterOperator.Equal, model.Id));
        }
    }
}