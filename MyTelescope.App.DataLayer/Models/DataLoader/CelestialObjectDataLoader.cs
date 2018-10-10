namespace MyTelescope.App.DataLayer.Models.DataLoader
{
    using System.Collections.Generic;
    using MyTelescope.Utilities.Interfaces.Connector;
    using MyTelescope.Utilities.Models.Filter;
    using MyTelescope.Utilities.Models.Sort;
    using SolarSystem.Models.CelestialObject;
    using ViewModels.Helpers.Filter;
    using ViewModels.Models.Item;

    public class CelestialObjectDataLoader : HttpDataLoader<CelestialObjectViewModel, CelestialObjectModel>
    {
        public CelestialObjectDataLoader(IConnector<CelestialObjectModel> connector) 
            : base(connector)
        {
        }

        protected override SortModel GetSort()
        {
            return new SortModel($"{nameof(CelestialObjectModel.SemiMajorAxis)}", true);
        }

        protected override List<FilterItemModel> GetModelFilterItems(CelestialObjectModel model)
        {
            return model == null 
                ? base.GetModelFilterItems(null)
                : new List<FilterItemModel> { CelestialObjectFilterHelper.GetParentCelestialObjectFilter(model.Id) };
        }
    }
}
