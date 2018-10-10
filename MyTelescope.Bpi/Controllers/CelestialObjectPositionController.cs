namespace MyTelescope.Api.Controllers
{
    using DataLayer.Models;
    using Microsoft.AspNetCore.Mvc;
    using Utilities.Interfaces.Connector;

    [Route("api/[controller]")]
    public class CelestialObjectPositionController : DataController<CelestialObjectPositionModel>
    {
        public CelestialObjectPositionController(IContextConnector<CelestialObjectPositionModel> connector)
            : base(connector)
        {
        }
    }
}