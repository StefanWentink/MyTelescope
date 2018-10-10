namespace MyTelescope.Api.Controllers
{
    using DataLayer.Models;
    using Microsoft.AspNetCore.Mvc;
    using Utilities.Interfaces.Connector;

    [Route("api/[controller]")]
    public class CelestialObjectTypeController : DataController<CelestialObjectTypeModel>
    {
        public CelestialObjectTypeController(IContextConnector<CelestialObjectTypeModel> connector)
            : base(connector)
        {
        }
    }
}