namespace MyTelescope.Api.DataLayer.Factories
{
    using SolarSystem.Enums;
    using SolarSystem.Extensions;
    using SolarSystem.Models.CelestialObject;
    using Utilities.Interfaces.Connector;

    public class CelestialObjectTypeFactory : SingletonFactory<CelestialObjectType>
    {
        public static CelestialObjectTypeFactory Instance { get; private set; }

        public CelestialObjectTypeFactory(IConnector<CelestialObjectType> connector)
            : base(connector)
        {
            if (Instance == null)
            {
                Instance = this;
            }
        }

        public CelestialObjectType GetSingleByEnum(CelestialType value)
        {
            var celestialObjectTypeCode = value.ToConstant();
            return GetSingleByCode(celestialObjectTypeCode);
        }

        public CelestialObjectType GetSingleByCode(string value)
        {
            return GetSingleByFunction(x => x.Code.Equals(value));
        }
    }
}