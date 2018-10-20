namespace MyTelescope.App.OData.Models.Policies
{
    using Microsoft.Extensions.Logging;
    using MyTelescope.SolarSystem.Models.CelestialObject;

    public class CelestialObjectPolicy : MyTelescopePolicy<CelestialObject>
    {
        public CelestialObjectPolicy(ILogger logger)
            : base(logger, 30000)
        {
        }
    }
}