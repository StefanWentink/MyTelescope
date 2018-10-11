namespace MyTelescope.Seeder
{
    using Api.DataLayer.Connectors;
    using Api.DataLayer.Context;
    using Core.Utilities.Helpers;
    using Ef.Utilities.Interfaces;
    using Helpers;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using SolarSystem.Constants;
    using SolarSystem.Models.CelestialObject;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Utilities.Enums;
    using Utilities.Helpers;
    using Utilities.Interfaces.Connector;
    using Utilities.Models.Filter;

    public class Program
    {
        private static ServiceProvider ServiceProvider { get; set; }

        public static void Main(string[] args)
        {
            // setup our DI
            var serviceProvider = new ServiceCollection()
                .AddLogging()
                    .AddSingleton<IContextContainer, MyTelescopeContextContainer>()
                    .AddSingleton<IContextConnector<CelestialObjectTypeModel>, CelestialObjectTypeConnector>()
                    .AddSingleton<IContextConnector<CelestialObjectModel>, CelestialObjectConnector>()
                    .AddSingleton<IContextConnector<CelestialObjectPositionModel>, CelestialObjectPositionConnector>()
                    .BuildServiceProvider();

            ServiceProvider = serviceProvider;

            // configure console logging
            serviceProvider
                .GetService<ILoggerFactory>()
                .AddConsole(LogLevel.Debug);

            var logger = serviceProvider.GetService<ILoggerFactory>()
                .CreateLogger<Program>();

            LogHelper.Init(logger);

            var input = ReadInput();
            ProcessInput(input);

            LogHelper.LogInformation(string.Empty);
            LogHelper.LogInformation("Press any key to exit.");
            Console.Read();
        }

        private static void ProcessInput(int input)
        {
            var celestialObjectConnector = ServiceProvider.GetService<IContextConnector<CelestialObjectModel>>();
            List<CelestialObjectModel> celestialObjectSunResult = null;
            List<CelestialObjectModel> celestialObjectPlanetResult = null;
            List<CelestialObjectTypeModel> celestialObjectTypeResult = null;

            if (input.In(1, 9))
            {
                var celestialObjectTypeConnector = ServiceProvider.GetService<IContextConnector<CelestialObjectTypeModel>>();
                celestialObjectTypeResult = new CelestialObjectTypeSeeder(celestialObjectTypeConnector).Seed();

                if (celestialObjectTypeResult == null || celestialObjectTypeResult.Count == 0)
                {
                    celestialObjectTypeResult = celestialObjectTypeConnector.Read(new FilterModel());
                }

                celestialObjectSunResult = new CelestialObjectSunSeeder(celestialObjectConnector).Seed();

                if (celestialObjectSunResult == null || celestialObjectSunResult.Count == 0)
                {
                    var filter = new FilterModel(
                        new FilterItemModel(
                            nameof(CelestialObjectModel.CelestialObjectTypeId),
                            ColumnType.GuidColumn,
                            FilterType.Equal,
                            celestialObjectTypeResult.Single(x => x.Code == CelestialObjectTypeConstants.Star).Id));

                    celestialObjectSunResult = celestialObjectConnector.Read(filter);
                }

                celestialObjectPlanetResult = new CelestialObjectPlanetSeeder(celestialObjectConnector).Seed();
                celestialObjectPlanetResult = celestialObjectPlanetResult
                    .Where(x => x.CelestialObjectTypeId == celestialObjectTypeResult.Single(p => p.Code == CelestialObjectTypeConstants.Planet).Id).ToList();

                new CelestialObjectMoonSeeder(celestialObjectConnector, celestialObjectTypeResult, celestialObjectPlanetResult).Seed();
            }

            if (input.In(2, 9))
            {
                if (celestialObjectTypeResult == null || celestialObjectTypeResult.Count == 0)
                {
                    var celestialObjectTypeConnector = ServiceProvider.GetService<IContextConnector<CelestialObjectTypeModel>>();
                    celestialObjectTypeResult = celestialObjectTypeConnector.Read(new FilterModel());
                }

                // TODO
                if (celestialObjectPlanetResult == null)
                {
                    var filter = new FilterModel(
                        new FilterItemModel(
                            nameof(CelestialObjectModel.CelestialObjectTypeId),
                            ColumnType.GuidColumn,
                            FilterType.Equal,
                        celestialObjectTypeResult.Single(x => x.Code == CelestialObjectTypeConstants.Planet).Id));

                    celestialObjectPlanetResult = celestialObjectConnector.Read(filter);
                }

                if (celestialObjectPlanetResult == null)
                {
                    var filter = new FilterModel(
                        new FilterItemModel(
                            nameof(CelestialObjectModel.CelestialObjectTypeId),
                            ColumnType.GuidColumn,
                            FilterType.Equal,
                        celestialObjectTypeResult.Single(x => x.Code == CelestialObjectTypeConstants.Planet).Id));

                    celestialObjectPlanetResult = celestialObjectConnector.Read(filter);
                }

                var celestialObjectPositionConnector = ServiceProvider.GetService<IContextConnector<CelestialObjectPositionModel>>();
                new CelestialObjectPositionSeeder(celestialObjectPositionConnector, celestialObjectPlanetResult).Seed();
            }
        }

        private static int ReadInput()
        {
            LogHelper.LogInformation(string.Empty);
            LogHelper.LogInformation("Choose one of the following options.");
            LogHelper.LogInformation("1. Seed celestial objects.");
            LogHelper.LogInformation("2. Seed celestial data.");
            LogHelper.LogInformation("9. Seed all.");
            LogHelper.LogInformation(string.Empty);

            var key = Console.ReadKey();

            if (!key.KeyChar.In('1', '2', '9'))
            {
                LogHelper.LogInformation($"{key.KeyChar} input is invalid.");
                LogHelper.LogInformation(string.Empty);

                return ReadInput();
            }

            if (int.TryParse(key.KeyChar.ToString(), out int result))
            {
                return result;
            }

            return ReadInput();
        }
    }
}