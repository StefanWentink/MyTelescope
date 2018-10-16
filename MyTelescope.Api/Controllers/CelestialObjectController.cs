namespace MyTelescope.Api.Controllers
{
    using SolarSystem.Models.CelestialObject;
    using Utilities.Interfaces.Connector;

    public class CelestialObjectController : DataController<CelestialObject>
    {
        public CelestialObjectController(IContextConnector<CelestialObject> connector)
            : base(connector)
        {
        }
    }
}