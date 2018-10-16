namespace MyTelescope.App.OData.Models
{
    using SWE.Polly.Models.Policies;

    public class MyTelescopePolicy : RetryPolicy
    {
        public MyTelescopePolicy()
            : base(350, 200, 3, 2000)
        {
        }
    }
}