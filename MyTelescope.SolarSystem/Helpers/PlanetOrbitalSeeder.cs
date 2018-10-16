namespace MyTelescope.SolarSystem.Helpers
{
    using Enums;
    using Models.Keplerian;
    using System.Collections.Generic;

    public static class PlanetOrbitalSeeder
    {
        private static Dictionary<Celestial, KeplerianOrbitValueModel> _keplerianOrbitValues;

        public static KeplerianOrbitValueModel GetOrbitValues(this Celestial celestialObject)
        {
            if (KeplerianOrbitValues.ContainsKey(celestialObject))
            {
                return KeplerianOrbitValues[celestialObject];
            }

            return null;
        }

        private static Dictionary<Celestial, KeplerianOrbitValueModel> KeplerianOrbitValues =>
            _keplerianOrbitValues ?? (_keplerianOrbitValues = GetList());

        private static Dictionary<Celestial, KeplerianOrbitValueModel> GetList()
        {
            var result = new Dictionary<Celestial, KeplerianOrbitValueModel>
            {
                { Celestial.Mercury, new KeplerianOrbitValueModel(4.092317, 0.37073, 77.456) },
                { Celestial.Venus, new KeplerianOrbitValueModel(1.602136, 0.72330, 131.564) },
                { Celestial.Earth, new KeplerianOrbitValueModel(0.985608, 0.99972, 102.937) },
                { Celestial.Mars, new KeplerianOrbitValueModel(0.524039, 1.51039, 336.060) },
                { Celestial.Jupiter, new KeplerianOrbitValueModel(0.083056, 5.19037, 14.331) },
                { Celestial.Saturn, new KeplerianOrbitValueModel(0.033371, 9.52547, 93.057) },
                { Celestial.Uranus, new KeplerianOrbitValueModel(0.011698, 19.17725, 173.005) },
                { Celestial.Neptune, new KeplerianOrbitValueModel(0.005965, 30.10796, 48.124) },
                { Celestial.Pluto, new KeplerianOrbitValueModel(0.003964, 37.09129, 224.075) }
            };
            return result;
        }
    }
}