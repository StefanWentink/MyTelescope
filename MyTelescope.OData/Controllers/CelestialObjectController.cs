namespace MyTelescope.OData.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using MyTelescope.SolarSystem.Models.CelestialObject;
    using MyTelescope.Utilities.Interfaces.Connector;

    [ApiController]
    public class CelestialObjectController : DataController<CelestialObject>
    {
        public CelestialObjectController(IContextConnector<CelestialObject> connector)
            : base(connector)
        {
        }
    }
}