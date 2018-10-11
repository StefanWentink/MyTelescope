namespace MyTelescope.Api.Controllers
{
    using SolarSystem.Models.CelestialObject;
    using Utilities.Interfaces.Connector;

    public class CelestialObjectPositionController : DataController<CelestialObjectPositionModel>
    {
        public CelestialObjectPositionController(IContextConnector<CelestialObjectPositionModel> connector)
            : base(connector)
        {
        }
    }
}