namespace MyTelescope.SolarSystem.Extensions
{
    using System;
    using Constants;
    using Enums;

    public static class CelestialObjectTypeExtensions
    {
        public static string ToConstant(this CelestialObjectType value)
        {
            switch (value)
            {
                case CelestialObjectType.Star:
                    return CelestialObjectTypeConstants.Star;
                case CelestialObjectType.Planet:
                    return CelestialObjectTypeConstants.Planet;
                case CelestialObjectType.MajorMoon:
                    return CelestialObjectTypeConstants.MajorMoon;
                case CelestialObjectType.MinorMoon:
                    return CelestialObjectTypeConstants.MinorMoon;
                default:
                    throw new ArgumentException($"{value} is an invalid {nameof(CelestialObjectType)}", nameof(value));
            }
        }

        public static CelestialObjectType ToEnum(string value)
        {
            switch (value)
            {
                case CelestialObjectTypeConstants.Star:
                    return CelestialObjectType.Star;
                case CelestialObjectTypeConstants.Planet:
                    return CelestialObjectType.Planet;
                case CelestialObjectTypeConstants.MajorMoon:
                    return CelestialObjectType.MajorMoon;
                case CelestialObjectTypeConstants.MinorMoon:
                    return CelestialObjectType.MinorMoon;
                default:
                    throw new ArgumentException($"{value} is an invalid {nameof(CelestialObjectType)}", nameof(value));
            }
        }
    }
}
