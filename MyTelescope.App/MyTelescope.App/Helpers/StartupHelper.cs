namespace MyTelescope.App.Helpers
{
    using Di;
    using FreshMvvm;
    using Microsoft.Extensions.Logging;
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

            ResourceDiHelper.ConfigureServices();
            //DataDiHelper.ConfigureDataLayerServices();
            DataDiHelper.ConfigureODataServices();

            while (!result)
            {
                result = ConfigHelper.Initialized;
                ILoggerFactory loggerFactory = new LoggerFactory().AddDebug();
                FreshIOC.Container.Register<ILoggerFactory>(loggerFactory);
            }

            return result;
        }
    }
}