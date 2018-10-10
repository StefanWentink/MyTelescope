namespace MyTelescope.Api.Controllers
{
    using DataLayer.Models;
    using Microsoft.AspNetCore.Mvc;
    using MyTelescope.Utilities.Interfaces.Connector;

    [Route("api/[controller]")]
    public class CelestialObjectController : DataController<CelestialObjectModel>
    {
        public CelestialObjectController(IContextConnector<CelestialObjectModel> connector) 
            : base(connector)
        {
        }
    }
}