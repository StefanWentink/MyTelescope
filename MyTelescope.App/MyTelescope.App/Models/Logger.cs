namespace MyTelescope.App.Models
{
    using System;
    using Microsoft.Extensions.Logging;
    using MyTelescope.Core.Utilities.Helpers;

    public class Logger<T> : Logger, ILogger<T>
    {
    }

    public class Logger : ILogger
    {
        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            switch (logLevel)
            {
                case LogLevel.Critical:
                    LogHelper.LogCritical(exception.Message);
                    break;

                case LogLevel.Debug:
                    LogHelper.LogDebug(exception.Message);
                    break;

                case LogLevel.Error:
                    LogHelper.LogError(exception.Message);
                    break;

                case LogLevel.Warning:
                    LogHelper.LogWarning(exception.Message);
                    break;

                case LogLevel.Information:
                case LogLevel.None:
                case LogLevel.Trace:
                    LogHelper.LogInformation(exception.Message);
                    break;
            }
        }
    }
}