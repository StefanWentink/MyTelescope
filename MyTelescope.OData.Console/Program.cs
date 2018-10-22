namespace MyTelescope.OData.Console
{
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using MyTelescope.Core.Utilities.Helpers;
    using MyTelescope.SolarSystem.Models.CelestialObject;
    using MyTelescope.Utilities.Helpers;
    using SWE.Http.Interfaces;

    using SWE.OData.Builders;
    using SWE.Polly.Models;
    using System;
    using Newtonsoft.Json;
    using MyTelescope.App.OData.Models;
    using System.Threading;
    using SWE.OData.Models;
    using SWE.OData.Enums;
    using SWE.OData.Interfaces;
    using System.Collections.Generic;
    using MyTelescope.App.OData.Models.Policies;
    using MyTelescope.App.ViewModels.Models.Item;
    using MyTelescope.App.OData.Models.DataLoader;
    using MyTelescope.Data.Loader.Interfaces;
    using MyTelescope.SolarSystem.Helpers.Seeder;
    using MyTelescope.App.Utilities.EventArgs;
    using MyTelescope.App.Utilities.Models;
    using MyTelescope.App.Utilities.Interfaces;

    internal class Program
    {
        private static ServiceProvider InternalServiceProvider { get; set; }

        private static void Main(string[] args)
        {
            // setup our DI
            InternalServiceProvider = new ServiceCollection()
                .AddSingleton<ILogger>(new Logger())
                .AddSingleton<IExchanger, PolicyExchanger>()
                .AddSingleton<IBatchContainer, BatchContainer>()

                .AddSingleton<IRepository<CelestialObjectType>, ODataTypedRepository<CelestialObjectType>>()
                .AddSingleton<IRepository<CelestialObject>, ODataTypedRepository<CelestialObject>>()
                .AddSingleton<IRepository<CelestialObjectPosition>, ODataTypedRepository<CelestialObjectPosition>>()

                .AddSingleton<ITimeOutPolicy<CelestialObjectType>, CelestialObjectTypePolicy>()
                .AddSingleton<ITimeOutPolicy<CelestialObject>, CelestialObjectPolicy>()
                .AddSingleton<ITimeOutPolicy<CelestialObjectPosition>, CelestialObjectPositionPolicy>()

                .AddSingleton<IHttpDataLoader<CelestialObjectTypeViewModel, CelestialObjectType>, CelestialObjectTypeDataLoader>()
                .AddSingleton<IHttpDataLoader<CelestialObjectViewModel, CelestialObject>, CelestialObjectDataLoader>()
                .AddSingleton<IHttpDataLoader<CelestialObjectPositionViewModel, CelestialObjectPosition>, CelestialObjectPositionDataLoader>()

                .AddSingleton<IActions, MyTelescopeActions>()
                .AddSingleton<IUriContainer, MyTelescopeUriContainer>()
                .BuildServiceProvider();

            LogHelper.Init(new Logger());

            var input = ReadInput();

            while (input != 0)
            {
                ProcessInput(input);
                input = ReadInput();
            }
        }

        private static void ProcessInput(int input)
        {
            try
            {
                var cancellationToken = new CancellationToken();
                var result = string.Empty;

                if (input.In(1, 4, 7))
                {
                    var repository = InternalServiceProvider.GetService<IRepository<CelestialObjectType>>();
                    var builder = new ODataBuilder<CelestialObjectType, Guid>(nameof(CelestialObjectType));

                    if (input > 3)
                    {
                        var expand = new ODataBuilder<CelestialObject, Guid>(nameof(CelestialObject) + "s");

                        if (input > 6)
                        {
                            var filter = new ODataFilterSelector<CelestialObject, Guid>(x => x.CelestialObjectTypeId, FilterOperator.Equal, new Guid("f3c0940d-51bd-45da-90cc-f8ba8264abf9"));
                            var filters = new ODataFilters(filter);
                            filters.AddFilter(new ODataFilterSelector<CelestialObject, double>(x => x.Mass, FilterOperator.GreaterOrEquals, 0.8));
                            expand = expand.SetFilter(new ODataFilters(filters));
                            expand = expand.SetOrder<CelestialObject, double>(x => x.BlackBodyTemperature);
                            expand = expand.SetTop(3);
                        }

                        builder = builder.Expand(expand);
                    }

                    var content = builder.Build();

                    LogHelper.LogInformation($"CALL => {content}");
                    var objectTypes = repository.ReadAsync(cancellationToken, null, content).Result;
                    result = JsonConvert.SerializeObject(objectTypes);
                }

                if (input.In(2, 5))
                {
                    var repository = InternalServiceProvider.GetService<IRepository<CelestialObject>>();
                    IODataBuilder<CelestialObject, Guid> builder = new ODataBuilder<CelestialObject, Guid>(nameof(CelestialObject));

                    if (input > 3)
                    {
                        var filters = new List<IODataFilter>
                        {
                            new ODataFilterSelector<CelestialObject, Guid?>(x => x.CelestialObjectId, FilterOperator.Equal, new Guid("11A7011D-E239-420D-871A-0C1CC2ED0579")),
                            new ODataFilterSelector<CelestialObject, Guid>(x => x.CelestialObjectTypeId, FilterOperator.Equal, new Guid("90443d07-4a7f-493b-92fe-0810266ecef7"))
                        };
                        builder = builder.SetFilter(new ODataFilters(filters));
                    }

                    var content = builder.Build();
                    LogHelper.LogInformation($"CALL => {content}");
                    var objects = repository.ReadAsync(cancellationToken, null, content).Result;
                    result = JsonConvert.SerializeObject(objects);
                }

                if (input.In(3, 6))
                {
                    var repository = InternalServiceProvider.GetService<IRepository<CelestialObjectPosition>>();
                    var builder = new ODataBuilder<CelestialObjectPosition, Guid>(nameof(CelestialObjectPosition));

                    if (input > 3)
                    {
                        var filters = new List<IODataFilter>
                        {
                            new ODataFilterSelector<CelestialObjectPosition, Guid>(x => x.CelestialObjectId, FilterOperator.Equal, new Guid("11A7011D-E239-420D-871A-0C1CC2ED0579")),
                            new ODataFilterSelector<CelestialObjectPosition, DateTimeOffset>(x => x.ReferenceDate, FilterOperator.GreaterOrEquals, new DateTimeOffset(new DateTime(2018, 1, 1), TimeSpan.Zero)),
                            new ODataFilterSelector<CelestialObjectPosition, DateTimeOffset?>(x => x.ReferenceDate, FilterOperator.LessOrEquals, new DateTimeOffset(new DateTime(2018, 2, 1), TimeSpan.Zero))
                        };

                        builder = builder.SetFilter(new ODataFilters(filters));
                    }

                    var content = builder.Build();
                    LogHelper.LogInformation($"CALL => {content}");
                    var objectPositions = repository.ReadAsync(cancellationToken, null, content).Result;
                    result = JsonConvert.SerializeObject(objectPositions);
                }

                if (input.In(8))
                {
                    var dataLoader = InternalServiceProvider.GetService<IHttpDataLoader<CelestialObjectTypeViewModel, CelestialObjectType>>();

                    dataLoader.CollectionFetchedEvent +=
                        (object sender, CollectionFetchedEventArgs<CelestialObjectTypeViewModel> e) =>
                        {
                            var loadResult = JsonConvert.SerializeObject(e);
                            Console.WriteLine(loadResult);
                        };

                    dataLoader.Load(SWE.Http.Enums.DataLoading.Load, CelestialObjectTypeSeedHelper.GetCelestialObjectTypes()[0]);
                }

                if (input.In(9))
                {
                    var dataLoader = InternalServiceProvider.GetService<IHttpDataLoader<CelestialObjectViewModel, CelestialObject>>();

                    dataLoader.CollectionFetchedEvent +=
                        (object sender, CollectionFetchedEventArgs<CelestialObjectViewModel> e) =>
                        {
                            var loadResult = JsonConvert.SerializeObject(e);
                            Console.WriteLine(loadResult);
                        };

                    dataLoader.Load(SWE.Http.Enums.DataLoading.Load, null);
                }

                Console.WriteLine(result);
            }
            catch (Exception exception)
            {
                Console.WriteLine("EXCEPTION");
                Console.WriteLine(exception.Message);
            }
        }

        private static int ReadInput()
        {
            LogHelper.LogInformation(string.Empty);
            LogHelper.LogInformation("Choose one of the following options.");
            LogHelper.LogInformation("1. Read celestial types.");
            LogHelper.LogInformation("2. Read celestial objects.");
            LogHelper.LogInformation("3. Read celestial positions.");
            LogHelper.LogInformation("4. Read celestial types. With expand.");
            LogHelper.LogInformation("5. Read celestial objects. With filters.");
            LogHelper.LogInformation("6. Read celestial positions. With filters.");
            LogHelper.LogInformation("7. Read celestial types. With expand filters and order.");
            LogHelper.LogInformation("8. Data load celestial types.");
            LogHelper.LogInformation("9. Data load celestial objects.");
            LogHelper.LogInformation("0. Exit.");
            LogHelper.LogInformation(string.Empty);

            var key = Console.ReadKey();

            if (!key.KeyChar.In('0', '1', '2', '3', '4', '5', '6', '7', '8', '9'))
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