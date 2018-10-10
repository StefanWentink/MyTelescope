namespace MyTelescope.App.Helpers
{
    using System.Threading.Tasks;
    using Di;
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
            DataDiHelper.ConfigureServices();
            
            // TODO Logging

            while (!result)
            {
                result = ConfigHelper.Initialized;
            }

            return result;
        }
    }
}
