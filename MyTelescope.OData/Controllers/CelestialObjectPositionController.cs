namespace MyTelescope.OData.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using MyTelescope.SolarSystem.Models.CelestialObject;
    using MyTelescope.Utilities.Interfaces.Connector;

    [Route("api/[controller]")]
    [ApiController]
    public class CelestialObjectPositionController : DataController<CelestialObjectPositionModel>
    {
        public CelestialObjectPositionController(IContextConnector<CelestialObjectPositionModel> connector)
            : base(connector)
        {
        }
    }
}