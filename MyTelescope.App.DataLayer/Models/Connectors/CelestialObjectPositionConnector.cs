namespace MyTelescope.App.DataLayer.Models.Connectors
{
    using Interfaces;
    using SolarSystem.Models.CelestialObject;

    public class CelestialObjectPositionConnector : MyTelescopeConnector<CelestialObjectPosition>
    {
        public CelestialObjectPositionConnector(ICrudDataExchanger<IRequestModel> exchanger)
            : base(exchanger)
        {
        }
    }
}