namespace MyTelescope.SolarSystem.Extensions
{
    using Enums;
    using System;

    public static class CelestialObjectExtensions
    {
        public static string ToConstant(this Celestial value)
        {
            return value.ToString();
        }

        public static Celestial ToEnum(string value)
        {
            return (Celestial)Enum.Parse(typeof(Celestial), value);
        }
    }
}