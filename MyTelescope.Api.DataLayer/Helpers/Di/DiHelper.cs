﻿namespace MyTelescope.Api.DataLayer.Helpers.Di
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
            serviceCollection.AddScoped<IContextContainer, MyTelescopeContextContainer>();

            serviceCollection.AddScoped<IConnector<CelestialObjectType>, CelestialObjectTypeConnector>();
            serviceCollection.AddScoped<IConnector<CelestialObject>, CelestialObjectConnector>();
            serviceCollection.AddScoped<IConnector<CelestialObjectPosition>, CelestialObjectPositionConnector>();

            serviceCollection.AddScoped<IContextConnector<CelestialObjectType>, CelestialObjectTypeConnector>();
            serviceCollection.AddScoped<IContextConnector<CelestialObject>, CelestialObjectConnector>();
            serviceCollection.AddScoped<IContextConnector<CelestialObjectPosition>, CelestialObjectPositionConnector>();

            serviceCollection.AddSingleton<ISingletonFactory<CelestialObjectType>, CelestialObjectTypeFactory>();

            // Build the intermediate service provider
            var serviceProvider = serviceCollection.BuildServiceProvider();

            serviceProvider.GetRequiredService<ISingletonFactory<CelestialObjectType>>();
        }
    }
}