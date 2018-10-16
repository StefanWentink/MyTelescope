namespace MyTelescope.App.OData.Models.DataLoader
{
    using MyTelescope.Utilities.Interfaces.Connector;
    using MyTelescope.Utilities.Models.Sort;
    using SolarSystem.Models.CelestialObject;
    using SWE.Http.Interfacess;
    using ViewModels.Models.Item;

    public class CelestialObjectTypeDataLoader : HttpDataLoader<CelestialObjectTypeViewModel, CelestialObjectType>
    {
        public CelestialObjectTypeDataLoader(IRepository repository)
            : base(repository)
        {
        }

        protected override SortModel GetSort()
        {
            return new SortModel(nameof(CelestialObjectType.Code), true);
        }
    }
}