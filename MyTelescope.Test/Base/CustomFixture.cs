namespace MyTelescope.Test.Base
{
    using Core.Utilities.Helpers;
    using Core.Utilities.Helpers.Config;
    using Microsoft.Extensions.Logging;
    using MyTelescope.Utilities.Helpers;
    using System;

    public class CustomFixture : IDisposable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomFixture"/> class.
        /// Runs once
        /// </summary>
        public CustomFixture()
        {
            InitializeConfig();
            InitializeLogger();
        }

        /// <summary>
        /// Runs every method
        /// </summary>
        public void RunMethod()
        {
            LogHelper.LogInformation("Fixture::RunMethod()");
        }

        protected virtual void InitializeConfig()
        {
            if (!ConfigHelper.Initialized)
            {
                var currentDir = DirectoryHelper.GetCurrentMainDirectory();
                ConfigHelper.Initialize(currentDir, "Development");
            }
        }

        private void InitializeLogger()
        {
            var loggerFactory = new LoggerFactory()
                .AddConsole()
                .AddDebug();
            var logger = loggerFactory.CreateLogger<CustomFixture>();

            LogHelper.Init(logger, false);
        }

        public void Dispose()
        {
            LogHelper.LogInformation("Fixture::Dispose()");
        }
    }
}