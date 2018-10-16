namespace MyTelescope.SolarSystem.Models.Keplerian
{
    using Enums;

    public class KeplerianModel
    {
        public Celestial SolarSystemObject { get; }

        public KeplerianValueModel BaseValues { get; }

        public KeplerianValueModel CenturyValues { get; }

        public KeplerianModel(
            Celestial solarSystemObject,
            KeplerianValueModel baseValues,
            KeplerianValueModel centuryValues)
        {
            SolarSystemObject = solarSystemObject;
            BaseValues = baseValues;
            CenturyValues = centuryValues;
        }
    }
}