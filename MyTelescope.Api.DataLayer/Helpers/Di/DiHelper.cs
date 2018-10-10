namespace MyTelescope.Api.DataLayer.Helpers.Di
{
    using Connectors;
    using Context;
    using Ef.Utilities.Interfaces;
    using Factories;
    using Interfaces;
    using Microsoft.Extensions.DependencyInjection;
    using SolarSystem.Models.CelestialObject;
    using Utilities.Interfaces.Connector;

    public static class DiHelper
    {
        public static void ConfigureContextServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IContextContainer, MyTelescopeContextContainer>();

            serviceCollection.AddSingleton<IConnector<CelestialObjectTypeModel>, CelestialObjectTypeConnector>();
            serviceCollection.AddSingleton<IConnector<CelestialObjectModel>, CelestialObjectConnector>();
            serviceCollection.AddSingleton<IConnector<CelestialObjectPositionModel>, CelestialObjectPositionConnector>();

            serviceCollection.AddSingleton<IContextConnector<CelestialObjectTypeModel>, CelestialObjectTypeConnector>();
            serviceCollection.AddSingleton<IContextConnector<CelestialObjectModel>, CelestialObjectConnector>();
            serviceCollection.AddSingleton<IContextConnector<CelestialObjectPositionModel>, CelestialObjectPositionConnector>();

            serviceCollection.AddSingleton<ISingletonFactory<CelestialObjectTypeModel>, CelestialObjectTypeFactory>();
            
            // Build the intermediate service provider
            var serviceProvider = serviceCollection.BuildServiceProvider();

            serviceProvider.GetRequiredService<ISingletonFactory<CelestialObjectTypeModel>>();
        }
    }
}
