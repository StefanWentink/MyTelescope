namespace MyTelescope.Core.Utilities.Helpers
{
    using Microsoft.Extensions.Logging;
    using System;

    public static class LogHelper
    {
        private static ILogger Logger { get; set; }

        private static readonly object LoggerLock = new object();

        public static bool Initialized => Logger != null;

        private static ILogger GetLogger()
        {
            if (!Initialized)
            {
                throw new ArgumentException("Logger not yet initialized");
            }

            return Logger;
        }

        public static void Init(ILogger logModel)
        {
            Init(logModel, true);
        }

        public static void Init(ILogger logModel, bool throwException)
        {
            lock (LoggerLock)
            {
                if (Initialized)
                {
                    if (throwException)
                    {
                        throw new ArgumentException("Logger already  initialized");
                    }
                }
                else
                {
                    Logger = logModel;
                    LogInformation("Logger initialized");
                }
            }
        }

        public static void LogDebug(string message)
        {
            GetLogger().LogDebug(message);
        }

        public static void LogInformation(string message)
        {
            GetLogger().LogInformation(message);
        }

        public static void LogWarning(string message)
        {
            GetLogger().LogWarning(message);
        }

        public static void LogError(string message)
        {
            GetLogger().LogError(message);
        }

        public static void LogError(Exception ex)
        {
            LogError(ex.Message);
            if (ex.InnerException != null)
            {
                LogError(ex.InnerException);
            }
        }

        public static void LogCritical(string message)
        {
            GetLogger().LogCritical(message);
        }

        public static void LogCritical(Exception ex)
        {
            LogCritical(ex.Message);
            if (ex.InnerException != null)
            {
                LogCritical(ex.InnerException);
            }
        }
    }
}