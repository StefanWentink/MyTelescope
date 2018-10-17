﻿namespace MyTelescope.OData.Console.Models
{
    using Microsoft.Extensions.Logging;
    using MyTelescope.SolarSystem.Models.CelestialObject;

    public class CelestialObjectPositionPolicy : MyTelescopePolicy<CelestialObjectPosition>
    {
        public CelestialObjectPositionPolicy(ILogger<CelestialObjectPositionPolicy> logger)
            : base(logger, 5000)
        {
        }
    }
}