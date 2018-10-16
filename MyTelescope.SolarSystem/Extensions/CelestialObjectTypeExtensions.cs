namespace MyTelescope.SolarSystem.Extensions
{
    using Constants;
    using Enums;
    using System;

    public static class CelestialObjectTypeExtensions
    {
        public static string ToConstant(this CelestialType value)
        {
            switch (value)
            {
                case CelestialType.Star:
                    return CelestialObjectTypeConstants.Star;

                case CelestialType.Planet:
                    return CelestialObjectTypeConstants.Planet;

                case CelestialType.MajorMoon:
                    return CelestialObjectTypeConstants.MajorMoon;

                case CelestialType.MinorMoon:
                    return CelestialObjectTypeConstants.MinorMoon;

                default:
                    throw new ArgumentException($"{value} is an invalid {nameof(CelestialType)}", nameof(value));
            }
        }

        public static CelestialType ToEnum(string value)
        {
            switch (value)
            {
                case CelestialObjectTypeConstants.Star:
                    return CelestialType.Star;

                case CelestialObjectTypeConstants.Planet:
                    return CelestialType.Planet;

                case CelestialObjectTypeConstants.MajorMoon:
                    return CelestialType.MajorMoon;

                case CelestialObjectTypeConstants.MinorMoon:
                    return CelestialType.MinorMoon;

                default:
                    throw new ArgumentException($"{value} is an invalid {nameof(CelestialType)}", nameof(value));
            }
        }
    }
}