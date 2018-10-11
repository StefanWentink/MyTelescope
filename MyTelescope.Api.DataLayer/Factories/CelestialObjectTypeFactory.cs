namespace MyTelescope.Api.DataLayer.Factories
{
    using SolarSystem.Enums;
    using SolarSystem.Extensions;
    using SolarSystem.Models.CelestialObject;
    using Utilities.Interfaces.Connector;

    public class CelestialObjectTypeFactory : SingletonFactory<CelestialObjectTypeModel>
    {
        public static CelestialObjectTypeFactory Instance { get; private set; }

        public CelestialObjectTypeFactory(IConnector<CelestialObjectTypeModel> connector)
            : base(connector)
        {
            if (Instance == null)
            {
                Instance = this;
            }
        }

        public CelestialObjectTypeModel GetSingleByEnum(CelestialObjectType value)
        {
            var celestialObjectTypeCode = value.ToConstant();
            return GetSingleByCode(celestialObjectTypeCode);
        }

        public CelestialObjectTypeModel GetSingleByCode(string value)
        {
            return GetSingleByFunction(x => x.Code.Equals(value));
        }
    }
}