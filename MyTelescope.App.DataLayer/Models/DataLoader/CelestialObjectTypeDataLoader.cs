namespace MyTelescope.App.DataLayer.Models.DataLoader
{
    using MyTelescope.Utilities.Interfaces.Connector;
    using MyTelescope.Utilities.Models.Sort;
    using SolarSystem.Models.CelestialObject;
    using ViewModels.Models.Item;

    public class CelestialObjectTypeDataLoader : HttpDataLoader<CelestialObjectTypeViewModel, CelestialObjectTypeModel>
    {
        public CelestialObjectTypeDataLoader(IConnector<CelestialObjectTypeModel> connector)
            : base(connector)
        {
        }

        protected override SortModel GetSort()
        {
            return new SortModel($"{nameof(CelestialObjectTypeModel.Code)}", true);
        }
    }
}
