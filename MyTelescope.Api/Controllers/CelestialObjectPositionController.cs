namespace MyTelescope.Api.Controllers
{
    using SolarSystem.Models.CelestialObject;
    using Utilities.Interfaces.Connector;

    public class CelestialObjectPositionController : DataController<CelestialObjectPosition>
    {
        public CelestialObjectPositionController(IContextConnector<CelestialObjectPosition> connector)
            : base(connector)
        {
        }
    }
}