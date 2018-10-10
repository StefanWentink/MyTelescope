namespace MyTelescope.App.Localisation.Helpers.MyTelescope
{
    using global::MyTelescope.Utilities.Exceptions;
    using Resources.MyTelescope;
    using SolarSystem.Enums;

    public static class CelestialObjectTypeResourceHelper
    {
        public static string GetResourceSingular(this CelestialObjectType celestialObjectType)
        {
            switch (celestialObjectType)
            {
                case CelestialObjectType.Star:
                    return TextResource.Star;
                case CelestialObjectType.Planet:
                    return TextResource.Planet;
                case CelestialObjectType.MajorMoon:
                    return TextResource.MajorMoon;
                case CelestialObjectType.MinorMoon:
                    return TextResource.MinorMoon;
                default:
                    throw new EnumArgumentException<CelestialObjectType>((int)celestialObjectType);
            }
        }

        public static string GetResourcePlural(this CelestialObjectType celestialObjectType)
        {
            switch (celestialObjectType)
            {
                case CelestialObjectType.Star:
                    return TextResource.Stars;
                case CelestialObjectType.Planet:
                    return TextResource.Planets;
                case CelestialObjectType.MajorMoon:
                    return TextResource.MajorMoons;
                case CelestialObjectType.MinorMoon:
                    return TextResource.MinorMoons;
                default:
                    throw new EnumArgumentException<CelestialObjectType>((int)celestialObjectType);
            }
        }
    }
}
