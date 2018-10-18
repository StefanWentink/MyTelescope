namespace MyTelescope.App.OData.Models.Policies
{
    using Microsoft.Extensions.Logging;
    using MyTelescope.SolarSystem.Models.CelestialObject;

    public class CelestialObjectTypePolicy : MyTelescopePolicy<CelestialObjectType>
    {
        public CelestialObjectTypePolicy(ILogger<CelestialObjectTypePolicy> logger)
            : base(logger, 1000)
        {
        }
    }
}