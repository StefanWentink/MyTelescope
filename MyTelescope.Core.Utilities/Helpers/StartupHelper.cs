namespace MyTelescope.Core.Utilities.Helpers
{
    using System;
    using System.Reflection;
    using Config;
    using Microsoft.Extensions.Logging;
    using MyTelescope.Utilities.Helpers;

    public static class StartupHelper
    {
        /// <summary>
        /// Setup Logging an Configuration
        /// </summary>
        public static void Initialize()
        {
            var basePath = DirectoryHelper.GetCurrentMainDirectory();
            var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            ConfigHelper.Initialize(basePath, environmentName);

            var logFilePathFormat = $"Logs/{EntryAssemblyName}_{DateTime.UtcNow.Date}.log";
            var loggerFactory = new LoggerFactory()
                .AddConsole()
                .AddDebug()
                .AddFile(logFilePathFormat);

            var logger = loggerFactory.CreateLogger<Exception>();
            LogHelper.Init(logger, false);
        }

        private static string EntryAssemblyName => Assembly.GetEntryAssembly().GetName().Name;
    }
}
