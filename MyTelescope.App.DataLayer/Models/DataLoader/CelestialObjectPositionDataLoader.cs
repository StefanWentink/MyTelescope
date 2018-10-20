namespace MyTelescope.App.DataLayer.Models.DataLoader
{
    using MyTelescope.App.Utilities.Interfaces;
    using MyTelescope.Utilities.Interfaces.Connector;
    using MyTelescope.Utilities.Models.Filter;
    using MyTelescope.Utilities.Models.Sort;
    using SolarSystem.Models.CelestialObject;
    using System.Collections.Generic;
    using ViewModels.Helpers.Filter;
    using ViewModels.Models.Item;

    public class CelestialObjectPositionDataLoader : HttpDataLoader<CelestialObjectPositionViewModel, CelestialObjectPosition>
    {
        public CelestialObjectPositionDataLoader(IConnector<CelestialObjectPosition> connector, IBatchContainer batchContainer)
            : base(connector, batchContainer)
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

        protected override List<FilterItemModel> GetModelFilterItems(CelestialObjectPosition model)
        {
            return model == null
                ? base.GetModelFilterItems(null)
                : GetReferenceFilterItems(model);
        }

        private List<FilterItemModel> GetReferenceFilterItems(CelestialObjectPosition model)
        {
            return model.ReferenceEndDate.HasValue
                    ? CelestialObjectPositionFilterHelper.GetDefaultFilterList(model.CelestialObjectId, model.ReferenceDate, model.ReferenceEndDate.Value)
                    : CelestialObjectPositionFilterHelper.GetDefaultFilterList(model.CelestialObjectId, model.ReferenceDate);
        }
    }
}