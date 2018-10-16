namespace MyTelescope.OData.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using MyTelescope.SolarSystem.Models.CelestialObject;
    using MyTelescope.Utilities.Interfaces.Connector;

    [ApiController]
    public class CelestialObjectTypeController : DataController<CelestialObjectType>
    {
        public CelestialObjectTypeController(IContextConnector<CelestialObjectType> connector)
            : base(connector)
        {
        }
    }
}