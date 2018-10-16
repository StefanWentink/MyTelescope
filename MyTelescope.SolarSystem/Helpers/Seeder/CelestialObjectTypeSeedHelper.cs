namespace MyTelescope.SolarSystem.Helpers.Seeder
{
    using Enums;
    using Extensions;
    using Models.CelestialObject;
    using System;
    using System.Collections.Generic;

    public static class CelestialObjectTypeSeedHelper
    {
        public static List<CelestialObjectType> GetCelestialObjectTypes()
        {
            return new List<CelestialObjectType>
            {
                new CelestialObjectType(CelestialType.Star.ToConstant()) { Id = new Guid("C0EB2837-D6EA-4B1A-AF8D-B59FE9BC1AC2") },
                new CelestialObjectType(CelestialType.Planet.ToConstant()) { Id = new Guid("F3C0940D-51BD-45DA-90CC-F8BA8264ABF9") },
                new CelestialObjectType(CelestialType.MajorMoon.ToConstant()) { Id = new Guid("90443D07-4A7F-493B-92FE-0810266ECEF7") },
                new CelestialObjectType(CelestialType.MinorMoon.ToConstant()) { Id = new Guid("FFECE4BA-DBFC-494C-AB80-E7E0C5382D73") }
            };
        }
    }
}