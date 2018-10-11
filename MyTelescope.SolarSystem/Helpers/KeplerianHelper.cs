namespace MyTelescope.SolarSystem.Helpers
{
    using System;
    using Utilities.Constants;
    using Utilities.Extensions;
    using Utilities.Helpers;
    using Utilities.Models;

    public static class KeplerianHelper
    {
        /// <summary>
        /// Gets the number of centuries between <see cref="referenceDate"/> and year 2000
        /// </summary>
        /// <param name="referenceDate"></param>
        /// <returns></returns>
        public static double GetJ2000CenturyFactor(DateTimeOffset referenceDate)
        {
            var difference = GetTimeSpanDifference(referenceDate);
            var yearsDifference = difference.TotalDays / CalculationConstants.AvererageDaysInYear;
            return yearsDifference / 100;
        }

        /// <summary>
        /// Gets the number of centuries between <see cref="referenceDate"/> and year 2000
        /// </summary>
        /// <param name="referenceDate"></param>
        /// <returns></returns>
        public static double GetDaysDifference(DateTimeOffset referenceDate)
        {
            var difference = GetTimeSpanDifference(referenceDate);
            return difference.TotalDays;
        }

        private static TimeSpan GetTimeSpanDifference(DateTimeOffset referenceDate)
        {
            return referenceDate - new DateTimeOffset(2000, 1, 1, 0, 0, 0, TimeSpan.Zero);
        }

        /// <summary>
        /// Compute the AverageCentricDistance 'ν'
        /// </summary>
        /// <param name="semiMajorAxis"></param>
        /// <param name="eccentricity"></param>
        /// <returns></returns>
        internal static double GetAverageCentricDistance(double semiMajorAxis, double eccentricity)
        {
            return semiMajorAxis * (1 - Math.Pow(eccentricity, 2));
        }

        /// <summary>
        /// Compute the angle in degrees per day 'n'
        /// </summary>
        /// <param name="semiMajorAxis"></param>
        /// <returns></returns>
        internal static double GetAnglePerDay(double semiMajorAxis)
        {
            return CalculationConstants.GaussianGravitationalConstant / Math.Pow(semiMajorAxis, 3d / 2);
        }

        /// <summary>
        /// Compute the mean anomaly 'M'
        /// </summary>
        /// <param name="meanLongitude"></param>
        /// <param name="perihelionLongitude"></param>
        /// <returns></returns>
        public static double GetMeanAnomaly(double meanLongitude, double perihelionLongitude)
        {
            var result = meanLongitude - perihelionLongitude;

            while (result < 0)
            {
                result += 360;
            }

            return (meanLongitude - perihelionLongitude) % 360;
        }

        /// <summary>
        /// Compute the mean anomaly 'M' for a
        /// </summary>
        /// <param name="meanAnomaly"></param>
        /// <param name="anglePerDay"></param>
        /// <param name="daysDifference"></param>
        /// <returns></returns>
        public static double GetMeanAnomalyTime(double meanAnomaly, double anglePerDay, double daysDifference)
        {
            return (meanAnomaly + (anglePerDay * daysDifference)) % 360;
        }

        /// <summary>
        /// Compute the eccentric anomaly 'E'
        /// </summary>
        /// <param name="eccentricity"></param>
        /// <param name="meanAnomaly"></param>
        /// <returns></returns>
        internal static double GetEccentricAnomaly(double eccentricity, DegreeModel meanAnomaly)
        {
            var iteration = 0;
            const int maxIterationsiteration = 1000000;
            var tolerance = Math.Pow(10, -10);

            var eccentricAnomalyDegrees = new DegreeModel(DegreeHelper.RadiansToDegrees(eccentricity));

            var result = meanAnomaly.Degrees + (eccentricAnomalyDegrees.Degrees * MathDegrees.Sin(meanAnomaly.Degrees));

            while (iteration < maxIterationsiteration)
            {
                var deltaMeanAnomaly = meanAnomaly.Degrees - (result - (eccentricAnomalyDegrees.Degrees * MathDegrees.Sin(result)));
                var deltaEccentricAnomaly = deltaMeanAnomaly / (1 - (eccentricity * MathDegrees.Cos(result)));
                result += deltaEccentricAnomaly;

                if (Math.Abs(deltaEccentricAnomaly) < tolerance)
                {
                    return result;
                }

                iteration++;
            }

            return result;
        }

        /// <summary>
        /// Compute the eccentric anomaly 'E'
        /// </summary>
        /// <param name="eccentricity"></param>
        /// <param name="meanAnomaly"></param>
        /// <returns></returns>
        internal static double GetEccentricAnomaly2(double eccentricity, DegreeModel meanAnomaly)
        {
            return GetEccentricAnomaly2(eccentricity, meanAnomaly, 10, 1000000);
        }

        /// <summary>
        /// Compute the eccentric anomaly 'E'
        /// </summary>
        /// <param name="eccentricity"></param>
        /// <param name="meanAnomaly"></param>
        /// <param name="tolerance">number of decimal positions</param>
        /// <param name="maximumIterations"></param>
        /// <returns></returns>
        internal static double GetEccentricAnomaly2(double eccentricity, DegreeModel meanAnomaly, double tolerance, double maximumIterations)
        {
            var delta = Math.Pow(10, -tolerance);

            var ma = meanAnomaly.Degrees;
            ma /= 360.0;

            ma = 2 * Math.PI * (ma - Math.Floor(ma));

            var eccentricAnomaly = eccentricity < 0.8 ? ma : Math.PI;

            var calculateF = eccentricAnomaly - (eccentricity * Math.Sin(ma)) - ma;

            double i = 0;

            while (Math.Abs(calculateF) > delta && i < maximumIterations)
            {
                eccentricAnomaly -= (calculateF / (1.0 - (eccentricity * Math.Cos(eccentricAnomaly))));
                calculateF = eccentricAnomaly - (eccentricity * Math.Sin(eccentricAnomaly)) - ma;
                i++;
            }

            eccentricAnomaly = DegreeHelper.RadiansToDegrees(eccentricAnomaly);

            var result = Math.Round(eccentricAnomaly * Math.Pow(10, tolerance)) / Math.Pow(10, tolerance);

            return result;
        }

        public static double GetCentricDistance(double averageCentricDistance, double eccentricity, DegreeModel trueAnomaly)
        {
            return averageCentricDistance / (1 + (eccentricity * trueAnomaly.Cos()));
        }

        public static LocationModel GetLocation(double centricDistance, DegreeModel ascendingNodeLongitude, DegreeModel perihelionOmega, DegreeModel trueAnomaly, DegreeModel inclination)
        {
            var x = centricDistance * (
                        (ascendingNodeLongitude.Cos() * MathDegrees.Cos(perihelionOmega.Degrees + trueAnomaly.Degrees))
                        - (ascendingNodeLongitude.Sin() * inclination.Cos() * MathDegrees.Sin(perihelionOmega.Degrees + trueAnomaly.Degrees)));

            var y = centricDistance * (
                        (ascendingNodeLongitude.Sin() * MathDegrees.Cos(perihelionOmega.Degrees + trueAnomaly.Degrees))
                        + (ascendingNodeLongitude.Cos() * inclination.Cos() * MathDegrees.Sin(perihelionOmega.Degrees + trueAnomaly.Degrees)));

            var z = centricDistance * (inclination.Sin() * MathDegrees.Sin(perihelionOmega.Degrees + trueAnomaly.Degrees));

            return new LocationModel(x, y, z);
        }
    }
}