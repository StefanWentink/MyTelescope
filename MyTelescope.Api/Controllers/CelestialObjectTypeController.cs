namespace MyTelescope.Api.Controllers
{
    using SolarSystem.Models.CelestialObject;
    using Utilities.Interfaces.Connector;

    public class CelestialObjectTypeController : DataController<CelestialObjectTypeModel>
    {
        public CelestialObjectTypeController(IContextConnector<CelestialObjectTypeModel> connector)
            : base(connector)
        {
        }
    }
}