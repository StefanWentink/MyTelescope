namespace MyTelescope.OData.Console.Models
{
    using Microsoft.Extensions.Logging;
    using SWE.Http.Interfaces;
    using SWE.Polly.Models.Policies;

    public class MyTelescopePolicy<T> : PollyRetryPolicy, ITimeOutPolicy<T>
    {
        public MyTelescopePolicy(
            ILogger<MyTelescopePolicy<T>> logger,
            int timeOutMilliseconds)
            : base(logger, timeOutMilliseconds, 100, 3, 1000)
        {
        }
    }
}