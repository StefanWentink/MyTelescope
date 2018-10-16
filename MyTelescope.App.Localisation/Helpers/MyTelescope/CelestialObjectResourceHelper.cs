namespace MyTelescope.App.Localisation.Helpers.MyTelescope
{
    using global::MyTelescope.Utilities.Exceptions;
    using Resources.MyTelescope;
    using SolarSystem.Enums;

    public static class CelestialObjectTypeResourceHelper
    {
        public static string GetResourceSingular(this CelestialType celestialObjectType)
        {
            switch (celestialObjectType)
            {
                case CelestialType.Star:
                    return TextResource.Star;
                case CelestialType.Planet:
                    return TextResource.Planet;
                case CelestialType.MajorMoon:
                    return TextResource.MajorMoon;
                case CelestialType.MinorMoon:
                    return TextResource.MinorMoon;
                default:
                    throw new EnumArgumentException<CelestialType>((int)celestialObjectType);
            }
        }

        public static string GetResourcePlural(this CelestialType celestialObjectType)
        {
            switch (celestialObjectType)
            {
                case CelestialType.Star:
                    return TextResource.Stars;
                case CelestialType.Planet:
                    return TextResource.Planets;
                case CelestialType.MajorMoon:
                    return TextResource.MajorMoons;
                case CelestialType.MinorMoon:
                    return TextResource.MinorMoons;
                default:
                    throw new EnumArgumentException<CelestialType>((int)celestialObjectType);
            }
        }
    }
}
