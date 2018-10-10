namespace MyTelescope.App.Localisation.Helpers.MyTelescope
{
    using global::MyTelescope.Utilities.Exceptions;
    using Resources.MyTelescope;
    using SolarSystem.Enums;

    public static class PlanetResourceHelper
    {
        public static string GetResource(this CelestialObject celestialObject)
        {
            switch (celestialObject)
            {
                case CelestialObject.Sun:
                    return TextResource.Sun;
                case CelestialObject.Mercury:
                    return TextResource.Mercury;
                case CelestialObject.Venus:
                    return TextResource.Venus;
                case CelestialObject.Earth:
                    return TextResource.Earth;
                case CelestialObject.Moon:
                    return TextResource.Moon;
                case CelestialObject.Mars:
                    return TextResource.Mars;
                case CelestialObject.Phobos:
                    return TextResource.Phobos;
                case CelestialObject.Deimos:
                    return TextResource.Deimos;
                case CelestialObject.Jupiter:
                    return TextResource.Jupiter;
                case CelestialObject.Ganymede:
                    return TextResource.Ganymede;
                case CelestialObject.Callisto:
                    return TextResource.Callisto;
                case CelestialObject.Io:
                    return TextResource.Io;
                case CelestialObject.Europa:
                    return TextResource.Europa;
                case CelestialObject.Amalthea:
                    return TextResource.Amalthea;
                case CelestialObject.Saturn:
                    return TextResource.Saturn;
                case CelestialObject.Uranus:
                    return TextResource.Uranus;
                case CelestialObject.Neptune:
                    return TextResource.Neptune;
                case CelestialObject.Pluto:
                    return TextResource.Pluto;
                default:
                    throw new EnumArgumentException<CelestialObjectType>((int)celestialObject);
            }
        }
    }
}
