namespace MyTelescope.SolarSystem.Extensions
{
    using System;
    using Constants;
    using Enums;

    public static class CelestialObjectExtensions
    {

        public static string ToConstant(this CelestialObject value)
        {
            return value.ToString();
        }

        public static CelestialObject ToEnum(string value)
        {
            return (CelestialObject)Enum.Parse(typeof(CelestialObject), value);
        }
    }
}
