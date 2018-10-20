namespace MyTelescope.App.OData.Models.Policies
{
    using Microsoft.Extensions.Logging;
    using SWE.Http.Interfaces;
    using SWE.Polly.Models.Policies;

    public class MyTelescopePolicy<T> : PollyRetryPolicy, ITimeOutPolicy<T>
    {
        public MyTelescopePolicy(
            ILogger logger,
            int timeOutMilliseconds)
            : base(logger, timeOutMilliseconds, 10000, 10, 1000)
        {
        }
    }
}