namespace MyTelescope.App.Utilities.Helpers
{
    using System;

    public static class LogHelper
    {
        public static void LogDebug(string message)
        {
            // TODO
        }

        public static void LogInformation(string message)
        {
            // TODO
        }

        public static void LogWarning(string message)
        {
            // TODO
        }

        public static void LogError(string message)
        {
            // TODO
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
            // TODO
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
