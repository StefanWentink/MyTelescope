namespace MyTelescope.App.Helpers.Di
{
    using FreshMvvm;
    using Microsoft.Extensions.DependencyInjection;
    using MyTelescope.Utilities.Interfaces.Connector;
    using Microsoft.Extensions.Logging;
    using MyTelescope.App.Models;
    using MyTelescope.App.OData.Models.Policies;
    using MyTelescope.Data.Loader.Interfaces;
    using SolarSystem.Models.CelestialObject;
    using SWE.Http.Interfaces;
    using SWE.OData.Models;
    using SWE.Polly.Models;
    using ViewModels.Models.Item;

    public static class DataDiHelper
    {
        public static void ConfigureLoggerServices()
        {
            FreshIOC.Container.Register<ILogger, Logger>().AsMultiInstance();
        }

        //public static void ConfigureDataLayerServices()
        //{
        //    FreshIOC.Container.Register<IRequestModel, DataLayer.Models.Http.HttpRequestModel>().AsMultiInstance();
        //    FreshIOC.Container.Register<ICrudDataExchanger<DataLayer.Interfaces.IRequestModel>, DataLayer.Models.Http.MyTelescopeDataExchanger>().AsMultiInstance();
        //    FreshIOC.Container.Register<IDataTransponder, DataLayer.Models.DataTransponder>().AsMultiInstance();

        //    FreshIOC.Container.Register<IConnector<CelestialObject>, DataLayer.Models.Connectors.CelestialObjectConnector>().AsMultiInstance();
        //    FreshIOC.Container.Register<IConnector<CelestialObjectPosition>, DataLayer.Models.Connectors.CelestialObjectPositionConnector>().AsMultiInstance();

        //    FreshIOC.Container.Register<IHttpDataLoader<CelestialObjectTypeViewModel, CelestialObjectType>, DataLayer.Models.DataLoader.CelestialObjectTypeDataLoader>().AsMultiInstance();
        //    FreshIOC.Container.Register<IHttpDataLoader<CelestialObjectViewModel, CelestialObject>, DataLayer.Models.DataLoader.CelestialObjectDataLoader>().AsMultiInstance();
        //    FreshIOC.Container.Register<IHttpDataLoader<CelestialObjectPositionViewModel, CelestialObjectPosition>, DataLayer.Models.DataLoader.CelestialObjectPositionDataLoader>().AsMultiInstance();

        //    FreshIOC.Container.Register<IStaticDataLoader<CelestialObjectViewModel, CelestialObject>, DataLayer.Models.DataLoader.PlanetDataLoader>().AsMultiInstance();

        //    FreshIOC.Container.Register<IStaticDataLoader<PlanetDetailViewModel, CelestialObject>, DataLayer.Models.DataLoader.PlanetDetailLoader>().AsMultiInstance();
        //    FreshIOC.Container.Register<IStaticDataLoader<MoonDetailViewModel, CelestialObject>, DataLayer.Models.DataLoader.MoonDetailLoader>().AsMultiInstance();
        //}

        public static void ConfigureODataServices()
        {
            FreshIOC.Container.Register<IExchanger, PolicyExchanger>().AsMultiInstance();
            FreshIOC.Container.Register<IRepository<CelestialObjectType>, ODataTypedRepository<CelestialObjectType>>().AsMultiInstance();
            FreshIOC.Container.Register<IRepository<CelestialObject>, ODataTypedRepository<CelestialObject>>().AsMultiInstance();
            FreshIOC.Container.Register<IRepository<CelestialObjectPosition>, ODataTypedRepository<CelestialObjectPosition>>().AsMultiInstance();
            FreshIOC.Container.Register<ITimeOutPolicy<CelestialObjectType>, CelestialObjectTypePolicy>();
            FreshIOC.Container.Register<ITimeOutPolicy<CelestialObject>, CelestialObjectPolicy>();
            FreshIOC.Container.Register<ITimeOutPolicy<CelestialObjectPosition>, CelestialObjectPositionPolicy>();
            FreshIOC.Container.Register<IActions, OData.Models.MyTelescopeActions>();
            FreshIOC.Container.Register<IUriContainer, OData.Models.MyTelescopeUriContainer>();

            FreshIOC.Container.Register<IHttpDataLoader<CelestialObjectTypeViewModel, CelestialObjectType>, OData.Models.DataLoader.CelestialObjectTypeDataLoader>().AsMultiInstance();
            FreshIOC.Container.Register<IHttpDataLoader<CelestialObjectViewModel, CelestialObject>, OData.Models.DataLoader.CelestialObjectDataLoader>().AsMultiInstance();
            FreshIOC.Container.Register<IHttpDataLoader<CelestialObjectPositionViewModel, CelestialObjectPosition>, OData.Models.DataLoader.CelestialObjectPositionDataLoader>().AsMultiInstance();

            FreshIOC.Container.Register<IStaticDataLoader<CelestialObjectViewModel, CelestialObject>, OData.Models.DataLoader.PlanetDataLoader>().AsMultiInstance();

            FreshIOC.Container.Register<IStaticDataLoader<PlanetDetailViewModel, CelestialObject>, OData.Models.DataLoader.PlanetDetailLoader>().AsMultiInstance();
            FreshIOC.Container.Register<IStaticDataLoader<MoonDetailViewModel, CelestialObject>, OData.Models.DataLoader.MoonDetailLoader>().AsMultiInstance();
        }
    }
}