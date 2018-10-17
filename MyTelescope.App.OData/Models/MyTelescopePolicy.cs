namespace MyTelescope.App.OData.Models
{
    using Microsoft.Extensions.Logging;
    using SWE.Polly.Models.Policies;

    public class MyTelescopePolicy : PollyRetryPolicy
    {
        public MyTelescopePolicy(Logger<MyTelescopePolicy> logger)
            : base(logger, 350, 200, 3, 2000)
        {
        }
    }
}