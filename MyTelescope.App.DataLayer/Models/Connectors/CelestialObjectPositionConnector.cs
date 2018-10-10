namespace MyTelescope.App.DataLayer.Models.Connectors
{
    using Interfaces;
    using SolarSystem.Models.CelestialObject;

    public class CelestialObjectPositionConnector : MyTelescopeConnector<CelestialObjectPositionModel>
    {
        public CelestialObjectPositionConnector(ICrudDataExchanger<IRequestModel> exchanger)
            : base(exchanger)
        {
        }
    }
}
