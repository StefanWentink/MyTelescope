namespace MyTelescope.SolarSystem.Extensions
{
    using Constants;
    using Enums;
    using Helpers;
    using Models.CelestialObject;
    using Models.Keplerian;
    using SWE.BasicType.Utilities;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Utilities.Helpers;
    using Utilities.Models;

    public static class CelestialObjectPositionExtensions
    {
        private static KeplerianOrbitValueModel _earthKeplerianOrbitValue;

        private static KeplerianOrbitValueModel EarthKeplerianOrbitValue =>
            _earthKeplerianOrbitValue ?? (_earthKeplerianOrbitValue = Celestial.Earth.GetOrbitValues());

        public static double GetSiderealTime(
            this CelestialObjectPosition celestialObjectPosition,
            CelestialObject celestialObject,
            KeplerianOrbitValueModel keplerianOrbitValue,
            DateTimeOffset referenceDate)
        {
            return celestialObjectPosition.GetCelestialObjectSiderealTime(celestialObject, keplerianOrbitValue, (int)Math.Round(referenceDate.TimeOfDay.TotalMinutes), referenceDate.Offset.Minutes);
        }

        public static double GetCelestialObjectSiderealTime(
            this CelestialObjectPosition celestialObjectPosition,
            CelestialObject celestialObject,
            KeplerianOrbitValueModel keplerianOrbitValue,
            int minute,
            int minuteOffset)
        {
            return celestialObjectPosition.GetCelestialObjectSiderealTime(celestialObject, keplerianOrbitValue, minute - minuteOffset);
        }

        public static double GetEarthSiderealTimeMidnight(
            this CelestialObjectPosition celestialObjectPosition)
        {
            return GetEarthSiderealTimeMidnight(celestialObjectPosition.MeanAnomaly, celestialObjectPosition.ReferenceDate);
        }

        public static double GetEarthSiderealTimeMidnight(
            double earthMeanAnomaly,
            DateTimeOffset referenceDate)
        {
            var minuteOffset = (int)Math.Round(referenceDate.Offset.TotalMinutes);
            return GetSiderealTime(earthMeanAnomaly, 0.25, EarthKeplerianOrbitValue, -minuteOffset);
        }

        public static IEnumerable<SideRealTimeModel> GetEarthSiderealTimePerDay(
            this CelestialObjectPosition celestialObjectPosition,
            Interval interval,
            int hourRange)
        {
            return GetEarthSiderealTimePerDay(
                celestialObjectPosition.MeanAnomaly,
                celestialObjectPosition.ReferenceDate,
                interval,
                hourRange);
        }

        public static IEnumerable<SideRealTimeModel> GetEarthSiderealTimePerDay(
            double earthMeanAnomaly,
            DateTimeOffset referenceDate,
            Interval interval,
            int hourRange)
        {
            var minutesOffset = (int)Math.Round(referenceDate.Offset.TotalMinutes);
            var minuteRange = hourRange * 60;

            for (var minutes = -minuteRange; minutes < minuteRange; minutes += (int)interval)
            {
                yield return new SideRealTimeModel(
                    referenceDate.AddMinutes(minutes),
                    GetSiderealTime(earthMeanAnomaly, Interval.Minute.GetDegreefactor(), EarthKeplerianOrbitValue, minutes - minutesOffset));
            }
        }

        /// <summary>
        /// Θ = MEarth + ΠEarth + 15° (t+tz) (mod360°)
        /// </summary>
        /// <param name="celestialObjectPosition">
        /// The celestial Object Position.</param>
        /// <param name="celestialObject">The celestial Object.</param>
        /// <param name="keplerianOrbitValue"></param>
        /// <param name="utcMinutes"></param>
        /// <returns></returns>
        private static double GetCelestialObjectSiderealTime(
            this CelestialObjectPosition celestialObjectPosition,
            CelestialObject celestialObject,
            KeplerianOrbitValueModel keplerianOrbitValue,
            int utcMinutes)
        {
            var minuteConstant = celestialObject.SiderealRotationPeriod / (int)Interval.Day;

            return celestialObjectPosition.GetSiderealTime(minuteConstant, keplerianOrbitValue, utcMinutes);
        }

        /// <summary>
        /// Θ = MEarth + ΠEarth + 15° (t+tz) (mod360°)
        /// </summary>
        /// <param name="celestialObjectPosition">
        /// The celestial Object Position.</param>
        /// <param name="minuteConstant">degree ratation per minute</param>
        /// <param name="keplerianOrbitValue"></param>
        /// <param name="utcMinutes"></param>
        /// <returns></returns>
        private static double GetSiderealTime(
            this CelestialObjectPosition celestialObjectPosition,
            double minuteConstant,
            KeplerianOrbitValueModel keplerianOrbitValue,
            int utcMinutes)
        {
            return GetSiderealTime(celestialObjectPosition.MeanAnomaly, minuteConstant, keplerianOrbitValue, utcMinutes);
        }

        /// <summary>
        /// Θ = MEarth + ΠEarth + 15° (t+tz) (mod360°)
        /// </summary>
        /// <param name="meanAnomaly"></param>
        /// <param name="minuteConstant">degree ratation per minute</param>
        /// <param name="keplerianOrbitValue"></param>
        /// <param name="utcMinutes"></param>
        /// <returns></returns>
        private static double GetSiderealTime(
            double meanAnomaly,
            double minuteConstant,
            KeplerianOrbitValueModel keplerianOrbitValue,
            int utcMinutes)
        {
            return DegreeHelper.Modulo360Absolute(meanAnomaly + keplerianOrbitValue.Pi + (minuteConstant * utcMinutes));
        }

        public static TimeSpan GetTransitTimeSpan(
            this CelestialObjectPosition celestialObjectPosition,
            CelestialObjectPosition earthPosition,
            double longitudeDegrees)
        {
            var degrees = celestialObjectPosition.GetTransitDegrees(
                earthPosition,
                longitudeDegrees);

            return DegreeHelper.DegreeToTimeSpan(degrees);
        }

        public static double GetTransitDegrees(
            this CelestialObjectPosition celestialObjectPosition,
            CelestialObjectPosition earthPosition,
            double longitudeDegrees)
        {
            return GetTransitDegrees(
                celestialObjectPosition.RightAscension,
                earthPosition.MeanAnomaly,
                longitudeDegrees);
        }

        public static double GetTransitDegrees(
            double celestialObjectRightAscension,
            double earthMeanAnomaly,
            double longitudeDegrees)
        {
            return DegreeHelper.Modulo360Absolute(celestialObjectRightAscension
                     + longitudeDegrees
                     - earthMeanAnomaly
                     - EarthKeplerianOrbitValue.Pi);
        }

        public static RiseTransitSetModel GetRiseTransitSet(
            this CelestialObjectPosition celestialObjectPosition,
            CelestialObjectPosition earthPosition,
            double longitudeDegrees,
            double latitudeDegrees)
        {
            var transitTimeSpan = celestialObjectPosition.GetTransitTimeSpan(earthPosition, longitudeDegrees);

            var transit = new DateTimeOffset(
                celestialObjectPosition.ReferenceDate.DateTime.Year,
                celestialObjectPosition.ReferenceDate.DateTime.Month,
                celestialObjectPosition.ReferenceDate.DateTime.Day,
                transitTimeSpan.Hours,
                transitTimeSpan.Minutes,
                transitTimeSpan.Seconds,
                transitTimeSpan.Milliseconds,
                TimeSpan.Zero);

            var hourHorizonDegrees = celestialObjectPosition.GetHourHorizon(latitudeDegrees, 0);
            var hourHorizonTimeSpan = DegreeHelper.DegreeToTimeSpan(hourHorizonDegrees);
            var rise = transit - hourHorizonTimeSpan;
            var set = transit + hourHorizonTimeSpan;

            return new RiseTransitSetModel(rise, transit, set);
        }

        private static double GetHourHorizon(
            this CelestialObjectPosition celestialObjectPosition,
            double latitudeDegrees,
            double h0)
        {
            if (CompareUtilities.EqualsWithinTolerance(h0, 0, 6))
            {
                return DegreeHelper.RadiansToDegrees(
                    Math.Acos(
                        -MathDegrees.Tan(latitudeDegrees)
                        * MathDegrees.Tan(celestialObjectPosition.Declination)));
            }

            return DegreeHelper.RadiansToDegrees(Math.Acos(
                (MathDegrees.Sin(h0) - (MathDegrees.Sin(latitudeDegrees) * MathDegrees.Sin(celestialObjectPosition.Declination)))
                / (MathDegrees.Cos(latitudeDegrees) * MathDegrees.Cos(celestialObjectPosition.Declination))));
        }

        public static IEnumerable<SkyPositionModel> GetSkyPositions(
            this CelestialObjectPosition celestialObjectPosition,
            double latitude,
            List<SideRealTimeModel> siderealTimes,
            RiseTransitSetModel transitModel)
        {
            foreach (var sideRealTime in siderealTimes.Where(x => x.DateTimeOffset >= transitModel.Rise
                                                                  && x.DateTimeOffset <= transitModel.Set))
            {
                yield return celestialObjectPosition.GetSkyPosition(latitude, sideRealTime);
            }
        }

        public static SkyPositionModel GetSkyPosition(
            this CelestialObjectPosition celestialObjectPosition,
            double latitude,
            SideRealTimeModel siderealTime)
        {
            return GetSkyPosition(celestialObjectPosition.Declination, latitude, siderealTime.DateTimeOffset, siderealTime.SideRealTime);
        }

        internal static SkyPositionModel GetSkyPosition(
            double declination,
            double latitude,
            DateTimeOffset dateTimeOffset,
            double siderealTime)
        {
            var azimuth =
                DegreeHelper.RadiansToDegrees(
                    Math.Atan2(
                        MathDegrees.Sin(siderealTime),
                        (MathDegrees.Cos(siderealTime) * MathDegrees.Sin(latitude)) - (MathDegrees.Tan(declination) * MathDegrees.Cos(latitude))));

            var height =
                DegreeHelper.RadiansToDegrees(
                    Math.Asin(
                        (MathDegrees.Sin(latitude) * MathDegrees.Sin(declination))
                        + (MathDegrees.Cos(latitude) * MathDegrees.Cos(declination) * MathDegrees.Cos(siderealTime))));

            return new SkyPositionModel(dateTimeOffset, height, azimuth);
        }
    }
}