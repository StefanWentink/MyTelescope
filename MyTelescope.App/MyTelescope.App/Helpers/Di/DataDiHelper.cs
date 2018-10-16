namespace MyTelescope.App.Helpers.Di
{
    using DataLayer.Interfaces;
    using DataLayer.Models;
    using DataLayer.Models.DataLoader;
    using DataLayer.Models.Http;
    using FreshMvvm;
    using MyTelescope.Utilities.Interfaces.Connector;
    using SolarSystem.Models.CelestialObject;
    using ViewModels.Models.Item;

    public static class DataDiHelper
    {
        public static void ConfigureServices()
        {
            FreshIOC.Container.Register<IRequestModel, HttpRequestModel>().AsMultiInstance();
            FreshIOC.Container.Register<ICrudDataExchanger<IRequestModel>, MyTelescopeDataExchanger>().AsMultiInstance();
            FreshIOC.Container.Register<IDataTransponder, DataTransponder>().AsMultiInstance();

            FreshIOC.Container.Register<IConnector<CelestialObject>, DataLayer.Models.Connectors.CelestialObjectConnector>().AsMultiInstance();
            FreshIOC.Container.Register<IConnector<CelestialObjectPosition>, DataLayer.Models.Connectors.CelestialObjectPositionConnector>().AsMultiInstance();

            FreshIOC.Container.Register<IHttpDataLoader<CelestialObjectTypeViewModel, CelestialObjectType>, CelestialObjectTypeDataLoader>().AsMultiInstance();
            FreshIOC.Container.Register<IHttpDataLoader<CelestialObjectViewModel, CelestialObject>, CelestialObjectDataLoader>().AsMultiInstance();
            FreshIOC.Container.Register<IHttpDataLoader<CelestialObjectPositionViewModel, CelestialObjectPosition>, CelestialObjectPositionDataLoader>().AsMultiInstance();

            FreshIOC.Container.Register<IStaticDataLoader<CelestialObjectViewModel, CelestialObject>, PlanetDataLoader>().AsMultiInstance();

            FreshIOC.Container.Register<IStaticDataLoader<PlanetDetailViewModel, CelestialObject>, PlanetDetailLoader>().AsMultiInstance();
            FreshIOC.Container.Register<IStaticDataLoader<MoonDetailViewModel, CelestialObject>, MoonDetailLoader>().AsMultiInstance();
        }
    }
}