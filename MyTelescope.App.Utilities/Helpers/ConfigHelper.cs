namespace MyTelescope.App.Utilities.Helpers
{
    using Interfaces;
    using Models;
    using Newtonsoft.Json;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Logging;

    public static class ConfigHelper
    {
        private const string ConfigFileName = "config.json";

        private const string PlatformFileName = "platform.json";

        private static IRootConfiguration _rootConfiguration;

        public static bool Initialized => _rootConfiguration != null;

        public static async Task Initialize(IFileReader configurationFile)
        {
            var rootConfigurationTask = Build(configurationFile).ConfigureAwait(false);
            _rootConfiguration = await rootConfigurationTask;
        }

        public static void Initialize(IRootConfiguration rootConfiguration)
        {
            _rootConfiguration = rootConfiguration;
        }

        public static IRootConfiguration GetConfigBuilder()
        {
            if (!Initialized)
            {
                throw new ArgumentException("Configuration not initialized");
            }

            return _rootConfiguration;
        }

        public static byte[] CleanByteOrderMark(this byte[] bytes)
        {
            var bom = new byte[] { 0xEF, 0xBB, 0xBF };

            return bytes.Take(3).SequenceEqual(bom)
                ? bytes.Skip(3).ToArray()
                : bytes;
        }

        public static async Task<IRootConfiguration> Build(IFileReader configurationFile)
        {
            var configurationFileTask = configurationFile.ReadAsString(ConfigFileName).ConfigureAwait(false);
            var platformFileTask = configurationFile.ReadAsString(PlatformFileName).ConfigureAwait(false);

            var configurationFileContent = await configurationFileTask;
            var platformFileContent = await platformFileTask;

            return DeserializeConfig(configurationFileContent, platformFileContent);
        }

        internal static IRootConfiguration DeserializeConfig(string configurationContent, string platformContent)
        {
            try
            {
                var configuration = JsonConvert.DeserializeObject<RootConfiguration>(configurationContent);
                configuration.Platform = JsonConvert.DeserializeObject<PlatformConfiguration>(platformContent);

                return configuration;
            }
            catch (Exception exception)
            {
                LogHelper.LogError(exception);
                throw;
            }
        }

        public static void Clear()
        {
            _rootConfiguration = null;
        }
    }
}