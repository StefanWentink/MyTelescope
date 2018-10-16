namespace MyTelescope.App.DataLayer.Models.DataLoader
{
    using MyTelescope.Utilities.Interfaces.Connector;
    using MyTelescope.Utilities.Models.Filter;
    using MyTelescope.Utilities.Models.Sort;
    using SolarSystem.Models.CelestialObject;
    using System.Collections.Generic;
    using ViewModels.Helpers.Filter;
    using ViewModels.Models.Item;

    public class CelestialObjectDataLoader : HttpDataLoader<CelestialObjectViewModel, CelestialObject>
    {
        public CelestialObjectDataLoader(IConnector<CelestialObject> connector)
            : base(connector)
        {
        }

        protected override SortModel GetSort()
        {
            return new SortModel(nameof(CelestialObject.SemiMajorAxis), true);
        }

        protected override List<FilterItemModel> GetModelFilterItems(CelestialObject model)
        {
            return model == null
                ? base.GetModelFilterItems(null)
                : new List<FilterItemModel> { CelestialObjectFilterHelper.GetParentCelestialObjectFilter(model.Id) };
        }
    }
}