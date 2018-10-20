namespace MyTelescope.App.Utilities.Models
{
    using System;
    using Microsoft.Extensions.Logging;
    using MyTelescope.Core.Utilities.Helpers;

    public class Logger<T> : Logger, ILogger<T>
    {
    }

    public class Logger : ILogger
    {
        public Logger()
        { }

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
            Console.WriteLine(state);

            switch (logLevel)
            {
                case LogLevel.Critical:
                case LogLevel.Debug:
                case LogLevel.Error:
                case LogLevel.Warning:
                case LogLevel.Information:
                case LogLevel.None:
                case LogLevel.Trace:
                    break;
            }
        }
    }
}