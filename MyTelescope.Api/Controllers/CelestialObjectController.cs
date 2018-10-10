namespace MyTelescope.Api.Controllers
{
    using SolarSystem.Models.CelestialObject;
    using Utilities.Interfaces.Connector;
    
    public class CelestialObjectController : DataController<CelestialObjectModel>
    {
        public CelestialObjectController(IContextConnector<CelestialObjectModel> connector) 
            : base(connector)
        {
        }
    }
}