namespace MyTelescope.App.DataLayer.Models.DataLoader
{
    using MyTelescope.Utilities.Interfaces.Connector;
    using MyTelescope.Utilities.Models.Filter;
    using MyTelescope.Utilities.Models.Sort;
    using SolarSystem.Models.CelestialObject;
    using System.Collections.Generic;
    using ViewModels.Helpers.Filter;
    using ViewModels.Models.Item;

    public class CelestialObjectPositionDataLoader : HttpDataLoader<CelestialObjectPositionViewModel, CelestialObjectPositionModel>
    {
        public CelestialObjectPositionDataLoader(IConnector<CelestialObjectPositionModel> connector)
            : base(connector)
        {
        }

        protected override SortModel GetSort()
        {
            return new SortModel(
                new List<SortItemModel>
                {
                    new SortItemModel(nameof(CelestialObjectPositionModel.CelestialObjectId), true),
                    new SortItemModel(nameof(CelestialObjectPositionModel.ReferenceDate), true)
                });
        }

        protected override List<FilterItemModel> GetModelFilterItems(CelestialObjectPositionModel model)
        {
            return model == null
                ? base.GetModelFilterItems(null)
                : GetReferenceFilterItems(model);
        }

        private List<FilterItemModel> GetReferenceFilterItems(CelestialObjectPositionModel model)
        {
            return model.ReferenceEndDate.HasValue
                    ? CelestialObjectPositionFilterHelper.GetDefaultFilterList(model.CelestialObjectId, model.ReferenceDate, model.ReferenceEndDate.Value)
                    : CelestialObjectPositionFilterHelper.GetDefaultFilterList(model.CelestialObjectId, model.ReferenceDate);
        }
    }
}