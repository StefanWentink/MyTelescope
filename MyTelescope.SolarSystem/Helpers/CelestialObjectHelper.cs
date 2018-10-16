namespace MyTelescope.SolarSystem.Helpers
{
    using Enums;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Utilities.Helpers;

    public static class CelestialObjectHelper
    {
        public static List<Celestial> Planets { get; } =
            EnumHelper.GetValues<Celestial>(x => x.GetSolarSystemObjectType() == CelestialType.Planet).ToList();

        /// <summary>
        /// Gets a list of <see cref="CelestialType.MajorMoon"/> for a <see cref="CelestialType.Planet"/>
        /// </summary>
        /// <param name="solarSystemObject">Parse a <see cref="Celestial"/> of type <see cref="CelestialType.Planet"/></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">If <see cref="solarSystemObject"/> is not of type <see cref="CelestialType.Planet"/>.</exception>
        public static IEnumerable<Celestial> GetMainMoons(Celestial solarSystemObject)
        {
            return GetMoons(solarSystemObject, false);
        }

        /// <summary>
        /// Gets a list of <see cref="CelestialType.MajorMoon"/> and <see cref="CelestialType.MinorMoon"/> for a <see cref="CelestialType.Planet"/>
        /// </summary>
        /// <param name="solarSystemObject">Parse a <see cref="Celestial"/> of type <see cref="CelestialType.Planet"/></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">If <see cref="solarSystemObject"/> is not of type <see cref="CelestialType.Planet"/>.</exception>
        public static IEnumerable<Celestial> GetAllMoons(Celestial solarSystemObject)
        {
            return GetMoons(solarSystemObject, true);
        }

        /// <summary>
        /// Gets a list of <see cref="Celestial"/> for a <see cref="CelestialType.Planet"/>
        /// </summary>
        /// <param name="solarSystemObject">Parse a <see cref="Celestial"/> of type <see cref="CelestialType.Planet"/></param>
        /// <param name="includeMinorMoons">If true <see cref="CelestialType.MinorMoon"/> are included.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">If <see cref="solarSystemObject"/> is not of type <see cref="CelestialType.Planet"/>.</exception>
        private static IEnumerable<Celestial> GetMoons(this Celestial solarSystemObject, bool includeMinorMoons)
        {
            if (solarSystemObject.GetSolarSystemObjectType() != CelestialType.Planet)
            {
                throw new ArgumentException($"{solarSystemObject} is not of type {CelestialType.Planet}.", nameof(solarSystemObject));
            }

            var solarSolarSystemObjectTypes = new List<CelestialType> { CelestialType.MajorMoon };

            if (includeMinorMoons)
            {
                solarSolarSystemObjectTypes.Add(CelestialType.MinorMoon);
            }

            bool Expression(Celestial x) =>
                x > solarSystemObject
                && x < solarSystemObject + (int)CelestialType.Planet
                && solarSolarSystemObjectTypes.ToArray().Contains(x.GetSolarSystemObjectType());
            //&& x.GetSolarSystemObjectType().In(solarSolarSystemObjectTypes.ToArray());

            return EnumHelper.GetValues((Func<Celestial, bool>)Expression);
        }

        public static CelestialType GetSolarSystemObjectType(this Celestial solarSystemObject)
        {
            if (solarSystemObject == (int)CelestialType.Star)
            {
                return CelestialType.Star;
            }

            if ((int)solarSystemObject % (int)CelestialType.Planet == 0)
            {
                return CelestialType.Planet;
            }

            return (int)solarSystemObject % (CelestialType.MajorMoon - CelestialType.Planet) == 0 ?
                CelestialType.MajorMoon :
                CelestialType.MinorMoon;
        }
    }
}