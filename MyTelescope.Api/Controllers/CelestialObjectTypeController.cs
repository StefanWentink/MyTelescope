namespace MyTelescope.Api.Controllers
{
    using SolarSystem.Models.CelestialObject;
    using Utilities.Interfaces.Connector;

    public class CelestialObjectTypeController : DataController<CelestialObjectType>
    {
        public CelestialObjectTypeController(IContextConnector<CelestialObjectType> connector)
            : base(connector)
        {
        }
    }
}