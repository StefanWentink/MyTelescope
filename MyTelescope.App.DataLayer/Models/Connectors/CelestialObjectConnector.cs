namespace MyTelescope.App.DataLayer.Models.Connectors
{
    using Interfaces;
    using SolarSystem.Models.CelestialObject;

    public class CelestialObjectConnector : MyTelescopeConnector<CelestialObjectModel>
    {
        public CelestialObjectConnector(ICrudDataExchanger<IRequestModel> exchanger)
            : base(exchanger)
        {
        }
    }
}
