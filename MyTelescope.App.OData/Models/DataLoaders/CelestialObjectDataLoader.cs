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

    public class CelestialObjectDataLoader : HttpDataLoader<CelestialObjectViewModel, CelestialObject>
    {
        public CelestialObjectDataLoader(IRepository<CelestialObject> repository)
            : base(repository)
        {
        }

        protected override SortModel GetSort()
        {
            return new SortModel(nameof(CelestialObject.SemiMajorAxis), true);
        }

        protected override List<IODataFilter> GetModelFilters(CelestialObject model)
        {
            return model == null
                ? base.GetModelFilters(null)
                : new List<IODataFilter> { new ODataFilterSelector<CelestialObject, Guid?>(x => x.CelestialObjectId, FilterOperator.Equal, model.Id) };
        }
    }
}