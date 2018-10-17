namespace MyTelescope.OData.Console.Models
{
    using Microsoft.Extensions.Logging;
    using MyTelescope.SolarSystem.Models.CelestialObject;

    public class CelestialObjectPolicy : MyTelescopePolicy<CelestialObject>
    {
        public CelestialObjectPolicy(ILogger<CelestialObjectPolicy> logger)
            : base(logger, 2000)
        {
        }
    }
}