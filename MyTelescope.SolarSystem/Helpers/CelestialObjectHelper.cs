namespace MyTelescope.SolarSystem.Helpers
{
    using Enums;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Utilities.Helpers;

    public static class CelestialObjectHelper
    {
        public static List<CelestialObject> Planets { get; } =
            EnumHelper.GetValues<CelestialObject>(x => x.GetSolarSystemObjectType() == CelestialObjectType.Planet).ToList();

        /// <summary>
        /// Gets a list of <see cref="CelestialObjectType.MajorMoon"/> for a <see cref="CelestialObjectType.Planet"/>
        /// </summary>
        /// <param name="solarSystemObject">Parse a <see cref="CelestialObject"/> of type <see cref="CelestialObjectType.Planet"/></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">If <see cref="solarSystemObject"/> is not of type <see cref="CelestialObjectType.Planet"/>.</exception>
        public static IEnumerable<CelestialObject> GetMainMoons(CelestialObject solarSystemObject)
        {
            return GetMoons(solarSystemObject, false);
        }

        /// <summary>
        /// Gets a list of <see cref="CelestialObjectType.MajorMoon"/> and <see cref="CelestialObjectType.MinorMoon"/> for a <see cref="CelestialObjectType.Planet"/>
        /// </summary>
        /// <param name="solarSystemObject">Parse a <see cref="CelestialObject"/> of type <see cref="CelestialObjectType.Planet"/></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">If <see cref="solarSystemObject"/> is not of type <see cref="CelestialObjectType.Planet"/>.</exception>
        public static IEnumerable<CelestialObject> GetAllMoons(CelestialObject solarSystemObject)
        {
            return GetMoons(solarSystemObject, true);
        }

        /// <summary>
        /// Gets a list of <see cref="CelestialObject"/> for a <see cref="CelestialObjectType.Planet"/>
        /// </summary>
        /// <param name="solarSystemObject">Parse a <see cref="CelestialObject"/> of type <see cref="CelestialObjectType.Planet"/></param>
        /// <param name="includeMinorMoons">If true <see cref="CelestialObjectType.MinorMoon"/> are included.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">If <see cref="solarSystemObject"/> is not of type <see cref="CelestialObjectType.Planet"/>.</exception>
        private static IEnumerable<CelestialObject> GetMoons(this CelestialObject solarSystemObject, bool includeMinorMoons)
        {
            if (solarSystemObject.GetSolarSystemObjectType() != CelestialObjectType.Planet)
            {
                throw new ArgumentException($"{solarSystemObject} is not of type {CelestialObjectType.Planet}.", nameof(solarSystemObject));
            }

            var solarSolarSystemObjectTypes = new List<CelestialObjectType> { CelestialObjectType.MajorMoon };

            if (includeMinorMoons)
            {
                solarSolarSystemObjectTypes.Add(CelestialObjectType.MinorMoon);
            }

            bool Expression(CelestialObject x) =>
                x > solarSystemObject
                && x < solarSystemObject + (int)CelestialObjectType.Planet
                && solarSolarSystemObjectTypes.ToArray().Contains(x.GetSolarSystemObjectType());
            //&& x.GetSolarSystemObjectType().In(solarSolarSystemObjectTypes.ToArray());

            return EnumHelper.GetValues((Func<CelestialObject, bool>)Expression);
        }

        public static CelestialObjectType GetSolarSystemObjectType(this CelestialObject solarSystemObject)
        {
            if (solarSystemObject == (int)CelestialObjectType.Star)
            {
                return CelestialObjectType.Star;
            }

            if ((int)solarSystemObject % (int)CelestialObjectType.Planet == 0)
            {
                return CelestialObjectType.Planet;
            }

            return (int)solarSystemObject % (CelestialObjectType.MajorMoon - CelestialObjectType.Planet) == 0 ?
                CelestialObjectType.MajorMoon :
                CelestialObjectType.MinorMoon;
        }
    }
}