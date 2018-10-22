namespace MyTelescope.App.Helpers
{
    using Di;
    using FreshMvvm;
    using Microsoft.Extensions.Logging;
    using MyTelescope.App.Utilities.Models;
    using MyTelescope.Core.Utilities.Helpers;
    using System.Threading.Tasks;
    using Utilities.Helpers;
    using Utilities.Interfaces;
    using Xamarin.Forms;

    public static class StartupHelper
    {
        /// <summary>
        /// Setup Logging an Configuration
        /// </summary>
        public static bool Initialize()
        {
            var result = false;

            var configurationFile = DependencyService.Get<IFileConfiguration>();
            Task.Run(() => ConfigHelper.Initialize(configurationFile)).ConfigureAwait(false);

            //ILogger logger = new LoggerFactory()
            //    .AddDebug()
            //    .AddConsole()
            //    .CreateLogger<App>();

            LogHelper.Init(new Logger());

            ResourceDiHelper.ConfigureServices();
            //DataDiHelper.ConfigureDataLayerServices();
            DataDiHelper.ConfigureLoggerServices();
            DataDiHelper.ConfigureODataServices();

            while (!result)
            {
                result = ConfigHelper.Initialized;
            }

            DataDiHelper.ConfigureUriServices(ConfigHelper.RootConfiguration.Infrastructure.MyTeleScopeApi);

            return result;
        }
    }
}