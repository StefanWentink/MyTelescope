namespace MyTelescope.SolarSystem.Extensions
{
    using Enums;
    using System;

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