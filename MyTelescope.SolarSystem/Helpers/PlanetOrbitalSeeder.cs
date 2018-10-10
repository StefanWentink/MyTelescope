namespace MyTelescope.SolarSystem.Helpers
{
    using System.Collections.Generic;
    using Enums;
    using Models.Keplerian;

    public static class PlanetOrbitalSeeder
    {
        private static Dictionary<CelestialObject, KeplerianOrbitValueModel> _keplerianOrbitValues;

        public static KeplerianOrbitValueModel GetOrbitValues(this CelestialObject celestialObject)
        {
            if (KeplerianOrbitValues.ContainsKey(celestialObject))
            {
                return KeplerianOrbitValues[celestialObject];
            }

            return null;
        }

        private static Dictionary<CelestialObject, KeplerianOrbitValueModel> KeplerianOrbitValues => 
            _keplerianOrbitValues ?? (_keplerianOrbitValues = GetList());

        private static Dictionary<CelestialObject, KeplerianOrbitValueModel> GetList()
        {
            var result = new Dictionary<CelestialObject, KeplerianOrbitValueModel>();
            result.Add(CelestialObject.Mercury, new KeplerianOrbitValueModel(4.092317, 0.37073, 77.456));
            result.Add(CelestialObject.Venus, new KeplerianOrbitValueModel(1.602136, 0.72330, 131.564));
            result.Add(CelestialObject.Earth, new KeplerianOrbitValueModel(0.985608, 0.99972, 102.937));
            result.Add(CelestialObject.Mars, new KeplerianOrbitValueModel(0.524039, 1.51039, 336.060));
            result.Add(CelestialObject.Jupiter, new KeplerianOrbitValueModel(0.083056, 5.19037, 14.331));
            result.Add(CelestialObject.Saturn, new KeplerianOrbitValueModel(0.033371, 9.52547, 93.057));
            result.Add(CelestialObject.Uranus, new KeplerianOrbitValueModel(0.011698, 19.17725, 173.005));
            result.Add(CelestialObject.Neptune, new KeplerianOrbitValueModel(0.005965, 30.10796, 48.124));
            result.Add(CelestialObject.Pluto, new KeplerianOrbitValueModel(0.003964, 37.09129, 224.075));
            return result;
        }
    }
}
