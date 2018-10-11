﻿namespace MyTelescope.OData.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using MyTelescope.SolarSystem.Models.CelestialObject;
    using MyTelescope.Utilities.Interfaces.Connector;

    [ApiController]
    public class CelestialObjectController : DataController<CelestialObjectModel>
    {
        public CelestialObjectController(IContextConnector<CelestialObjectModel> connector)
            : base(connector)
        {
        }
    }
}