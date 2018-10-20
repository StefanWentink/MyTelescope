namespace MyTelescope.App.OData.Models.Policies
{
    using Microsoft.Extensions.Logging;
    using MyTelescope.SolarSystem.Models.CelestialObject;

    public class CelestialObjectTypePolicy : MyTelescopePolicy<CelestialObjectType>
    {
        public CelestialObjectTypePolicy(ILogger logger)
            : base(logger, 30000)
        {
        }
    }
}