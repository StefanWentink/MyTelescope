namespace MyTelescope.SolarSystem.Helpers.Seeder
{
    using System;
    using System.Collections.Generic;
    using Enums;
    using Extensions;
    using Models.CelestialObject;

    public static class CelestialObjectTypeSeedHelper
    {
        public static List<CelestialObjectTypeModel> GetCelestialObjectTypes()
        {
            return new List<CelestialObjectTypeModel>
            {
                new CelestialObjectTypeModel(CelestialObjectType.Star.ToConstant()) { Id = new Guid("C0EB2837-D6EA-4B1A-AF8D-B59FE9BC1AC2") },
                new CelestialObjectTypeModel(CelestialObjectType.Planet.ToConstant()) { Id = new Guid("F3C0940D-51BD-45DA-90CC-F8BA8264ABF9") },
                new CelestialObjectTypeModel(CelestialObjectType.MajorMoon.ToConstant()) { Id = new Guid("90443D07-4A7F-493B-92FE-0810266ECEF7") },
                new CelestialObjectTypeModel(CelestialObjectType.MinorMoon.ToConstant()) { Id = new Guid("FFECE4BA-DBFC-494C-AB80-E7E0C5382D73") }
            };
        }
    }
}
