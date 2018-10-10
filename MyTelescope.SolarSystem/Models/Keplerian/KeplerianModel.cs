namespace MyTelescope.SolarSystem.Models.Keplerian
{
    using Enums;

    public class KeplerianModel
    {
        public CelestialObject SolarSystemObject { get; }

        public KeplerianValueModel BaseValues { get; }

        public KeplerianValueModel CenturyValues { get; }

        public KeplerianModel(
            CelestialObject solarSystemObject,
            KeplerianValueModel baseValues,
            KeplerianValueModel centuryValues)
        {
            SolarSystemObject = solarSystemObject;
            BaseValues = baseValues;
            CenturyValues = centuryValues;
        }
    }
}