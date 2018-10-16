namespace MyTelescope.OData.Console.Models
{
    using Microsoft.Extensions.Logging;
    using SWE.Polly.Models.Policies;

    public class MyTelescopePolicy : RetryPolicy
    {
        public MyTelescopePolicy(ILogger<MyTelescopePolicy> logger)
            : base(logger, 350, 200, 3, 2000)
        {
        }
    }
}