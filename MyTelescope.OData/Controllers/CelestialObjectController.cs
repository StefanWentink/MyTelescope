namespace MyTelescope.OData.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using MyTelescope.SolarSystem.Models.CelestialObject;
    using MyTelescope.Utilities.Interfaces.Connector;

    [ApiController]
    public class CelestialObjectTypeController : DataController<CelestialObjectTypeModel>
    {
        public CelestialObjectTypeController(IContextConnector<CelestialObjectTypeModel> connector)
            : base(connector)
        {
        }
    }
}