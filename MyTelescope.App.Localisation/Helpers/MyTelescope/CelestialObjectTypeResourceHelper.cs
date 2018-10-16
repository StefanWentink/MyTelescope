namespace MyTelescope.App.Localisation.Helpers.MyTelescope
{
    using global::MyTelescope.Utilities.Exceptions;
    using Resources.MyTelescope;
    using SolarSystem.Enums;

    public static class PlanetResourceHelper
    {
        public static string GetResource(this Celestial celestialObject)
        {
            switch (celestialObject)
            {
                case Celestial.Sun:
                    return TextResource.Sun;
                case Celestial.Mercury:
                    return TextResource.Mercury;
                case Celestial.Venus:
                    return TextResource.Venus;
                case Celestial.Earth:
                    return TextResource.Earth;
                case Celestial.Moon:
                    return TextResource.Moon;
                case Celestial.Mars:
                    return TextResource.Mars;
                case Celestial.Phobos:
                    return TextResource.Phobos;
                case Celestial.Deimos:
                    return TextResource.Deimos;
                case Celestial.Jupiter:
                    return TextResource.Jupiter;
                case Celestial.Ganymede:
                    return TextResource.Ganymede;
                case Celestial.Callisto:
                    return TextResource.Callisto;
                case Celestial.Io:
                    return TextResource.Io;
                case Celestial.Europa:
                    return TextResource.Europa;
                case Celestial.Amalthea:
                    return TextResource.Amalthea;
                case Celestial.Saturn:
                    return TextResource.Saturn;
                case Celestial.Uranus:
                    return TextResource.Uranus;
                case Celestial.Neptune:
                    return TextResource.Neptune;
                case Celestial.Pluto:
                    return TextResource.Pluto;
                default:
                    throw new EnumArgumentException<CelestialType>((int)celestialObject);
            }
        }
    }
}
