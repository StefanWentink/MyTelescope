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

            serviceCollection.AddSingleton<IConnector<CelestialObjectType>, CelestialObjectTypeConnector>();
            serviceCollection.AddSingleton<IConnector<CelestialObject>, CelestialObjectConnector>();
            serviceCollection.AddSingleton<IConnector<CelestialObjectPosition>, CelestialObjectPositionConnector>();

            serviceCollection.AddSingleton<IContextConnector<CelestialObjectType>, CelestialObjectTypeConnector>();
            serviceCollection.AddSingleton<IContextConnector<CelestialObject>, CelestialObjectConnector>();
            serviceCollection.AddSingleton<IContextConnector<CelestialObjectPosition>, CelestialObjectPositionConnector>();

            serviceCollection.AddSingleton<ISingletonFactory<CelestialObjectType>, CelestialObjectTypeFactory>();

            // Build the intermediate service provider
            var serviceProvider = serviceCollection.BuildServiceProvider();

            serviceProvider.GetRequiredService<ISingletonFactory<CelestialObjectType>>();
        }
    }
}