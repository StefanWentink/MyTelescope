namespace MyTelescope.OData.Console
{
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using MyTelescope.Core.Utilities.Helpers;
    using MyTelescope.SolarSystem.Models.CelestialObject;
    using MyTelescope.Utilities.Helpers;
    using SWE.Http.Interfaces;
    using SWE.Http.Interfacess;
    using SWE.Http.Models;
    using SWE.OData.Builders;
    using SWE.Polly.Interfaces.Policies;
    using SWE.Polly.Models;
    using System;
    using Newtonsoft.Json;
    using MyTelescope.OData.Console.Models;
    using System.Threading;

    internal class Program
    {
        private static ServiceProvider InternalServiceProvider { get; set; }

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

            InternalServiceProvider = serviceProvider;

            // configure console logging
            serviceProvider
                .GetService<ILoggerFactory>()
                .AddConsole(LogLevel.Debug);

            var logger = serviceProvider.GetService<ILoggerFactory>()
                .CreateLogger<Program>();

            LogHelper.Init(logger);

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
                var repository = InternalServiceProvider.GetService<IRepository>();

                if (input.In(1, 4))
                {
                    var builder = new ODataBuilder<CelestialObjectType, Guid>(nameof(CelestialObjectType));
                    var content = builder.Build();
                    var objectTypes = repository.ReadAsync<CelestialObjectType>(cancellationToken, content).Result;
                    result = JsonConvert.SerializeObject(objectTypes);
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
            LogHelper.LogInformation("0. Exit.");
            LogHelper.LogInformation(string.Empty);

            var key = Console.ReadKey();

            if (!key.KeyChar.In('0', '1', '2', '3', '4', '5', '6', '9'))
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