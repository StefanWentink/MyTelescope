namespace MyTelescope.OData.Console
{
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using MyTelescope.App.OData.Models;
    using MyTelescope.Core.Utilities.Helpers;
    using SWE.Http.Interfaces;
    using SWE.Http.Interfacess;
    using SWE.Http.Models;
    using SWE.Polly.Interfaces.Policies;
    using SWE.Polly.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal class Program
    {
        private static void Main(string[] args)
        {
            // setup our DI
            var serviceProvider = new ServiceCollection()
                .AddLogging()
                    .AddSingleton<IRepository, Repository>()
                    .AddSingleton<IExchanger, PolicyExchanger>()
                    .AddSingleton<IPolicy, MyTelescopePolicy>()
                    .AddSingleton<IActions, MyTelescopeActions>()
                    .AddSingleton<IUriContainer, MyTelescopeUriContainer>()
                    .BuildServiceProvider();

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
            var celestialObjectConnector = ServiceProvider.GetService<IContextConnector<CelestialObject>>();
            List<CelestialObject> celestialObjectSunResult = null;
            List<CelestialObject> celestialObjectPlanetResult = null;
            List<CelestialObjectType> celestialObjectTypeResult = null;

            if (input.In(1, 9))
            {
                var celestialObjectTypeConnector = ServiceProvider.GetService<IContextConnector<CelestialObjectType>>();
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
                            nameof(CelestialObject.CelestialObjectTypeId),
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
                    var celestialObjectTypeConnector = ServiceProvider.GetService<IContextConnector<CelestialObjectType>>();
                    celestialObjectTypeResult = celestialObjectTypeConnector.Read(new FilterModel());
                }

                // TODO
                if (celestialObjectPlanetResult == null)
                {
                    var filter = new FilterModel(
                        new FilterItemModel(
                            nameof(CelestialObject.CelestialObjectTypeId),
                            ColumnType.GuidColumn,
                            FilterType.Equal,
                        celestialObjectTypeResult.Single(x => x.Code == CelestialObjectTypeConstants.Planet).Id));

                    celestialObjectPlanetResult = celestialObjectConnector.Read(filter);
                }

                if (celestialObjectPlanetResult == null)
                {
                    var filter = new FilterModel(
                        new FilterItemModel(
                            nameof(CelestialObject.CelestialObjectTypeId),
                            ColumnType.GuidColumn,
                            FilterType.Equal,
                        celestialObjectTypeResult.Single(x => x.Code == CelestialObjectTypeConstants.Planet).Id));

                    celestialObjectPlanetResult = celestialObjectConnector.Read(filter);
                }

                var celestialObjectPositionConnector = ServiceProvider.GetService<IContextConnector<CelestialObjectPosition>>();
                new CelestialObjectPositionSeeder(celestialObjectPositionConnector, celestialObjectPlanetResult).Seed();
            }
        }

        private static int ReadInput()
        {
            LogHelper.LogInformation(string.Empty);
            LogHelper.LogInformation("Choose one of the following options.");
            LogHelper.LogInformation("1. Seed celestial object types.");
            LogHelper.LogInformation("2. Seed celestial data.");
            LogHelper.LogInformation("3. Read celestial data.");
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