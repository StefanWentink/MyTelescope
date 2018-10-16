namespace MyTelescope.OData.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using MyTelescope.SolarSystem.Models.CelestialObject;
    using MyTelescope.Utilities.Interfaces.Connector;

    [ApiController]
    public class CelestialObjectPositionController : DataController<CelestialObjectPosition>
    {
        public CelestialObjectPositionController(IContextConnector<CelestialObjectPosition> connector)
            : base(connector)
        {
        }
    }
}