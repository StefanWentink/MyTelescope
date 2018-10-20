namespace MyTelescope.App.OData.Models.Policies
{
    using Microsoft.Extensions.Logging;
    using MyTelescope.SolarSystem.Models.CelestialObject;

    public class CelestialObjectPositionPolicy : MyTelescopePolicy<CelestialObjectPosition>
    {
        public CelestialObjectPositionPolicy(ILogger logger)
            : base(logger, 60000)
        {
        }
    }
}