namespace MyTelescope.Core.Utilities.Helpers.Config
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;

    public static class ConfigHelper
    {
        private static IConfigurationRoot _configurationBuilder;

        private static string _connectionString;

        private static string _apiMyTelescope;

        public static bool Initialized => _configurationBuilder != null;

        public static void Initialize(IHostingEnvironment env)
        {
            Initialize(env.ContentRootPath, env.EnvironmentName);
        }

        public static void Initialize(IConfigurationRoot config)
        {
            _configurationBuilder = config;
        }

        public static void Initialize(string rootPath, string environmentName)
        {
            _configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(rootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{ environmentName }.json", optional: true)
                .AddJsonFile("appsettings.local.json", optional: true)
                .Build();
        }

        public static IConfigurationRoot GetConfigBuilder()
        {
            if (!Initialized)
            {
                throw new ArgumentException("Configuration not initialized");
            }

            return _configurationBuilder;
        }

        public static string GetGenericConfigValue(List<string> path)
        {
            return GetConfigValue(GetConfigPathFromList(path));
        }

        public static string DefaultConnectionString => _connectionString ??
            (_connectionString = GetConfigValue("ConnectionString:Default"));

        public static string ApiMyTelescope => _apiMyTelescope ??
            (_apiMyTelescope = GetConfigValue("Api:MyTelescope"));

        public static void Clear()
        {
            _configurationBuilder = null;
            _connectionString = null;
        }

        private static string GetConfigPathFromList(IEnumerable<string> pathEntities)
        {
            var result = new StringBuilder();
            foreach (var entry in pathEntities)
            {
                if (!string.IsNullOrEmpty(result.ToString()))
                {
                    result.Append(":");
                }

                result.Append(entry);
            }

            return result.ToString();
        }

        private static string GetConfigValue(string key)
        {
            return GetConfigBuilder()[key];
        }
    }
}
