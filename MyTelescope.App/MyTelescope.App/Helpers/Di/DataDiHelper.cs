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

            FreshIOC.Container.Register<IConnector<CelestialObjectModel>, DataLayer.Models.Connectors.CelestialObjectConnector>().AsMultiInstance();
            FreshIOC.Container.Register<IConnector<CelestialObjectPositionModel>, DataLayer.Models.Connectors.CelestialObjectPositionConnector>().AsMultiInstance();
            
            FreshIOC.Container.Register<IHttpDataLoader<CelestialObjectTypeViewModel, CelestialObjectTypeModel>, CelestialObjectTypeDataLoader>().AsMultiInstance();
            FreshIOC.Container.Register<IHttpDataLoader<CelestialObjectViewModel, CelestialObjectModel>, CelestialObjectDataLoader>().AsMultiInstance();
            FreshIOC.Container.Register<IHttpDataLoader<CelestialObjectPositionViewModel, CelestialObjectPositionModel>, CelestialObjectPositionDataLoader>().AsMultiInstance();

            FreshIOC.Container.Register<IStaticDataLoader<CelestialObjectViewModel, CelestialObjectModel>, PlanetDataLoader>().AsMultiInstance();

            FreshIOC.Container.Register<IStaticDataLoader<PlanetDetailViewModel, CelestialObjectModel>, PlanetDetailLoader>().AsMultiInstance();
            FreshIOC.Container.Register<IStaticDataLoader<MoonDetailViewModel, CelestialObjectModel>, MoonDetailLoader>().AsMultiInstance();
        }
    }
}