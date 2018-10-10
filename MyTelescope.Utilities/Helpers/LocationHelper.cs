namespace MyTelescope.Utilities.Helpers
{
    using System;
    using Models;

    public static class LocationHelper
    {
        /// <summary>
        /// Determine delta of 2 location models
        /// </summary>
        /// <param name="model"></param>
        /// <param name="compare"></param>
        /// <returns></returns>
        public static LocationModel GetLocationDelta(this LocationModel model, LocationModel compare)
        {
            return new LocationModel(model.X - compare.X, model.Y - compare.Y, model.Z - compare.Z);
        }

        /// <summary>
        /// Get the LargeDelta 'Δ'
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static double GetLargeDelta(this LocationModel model)
        {
            return Math.Sqrt(Math.Pow(model.X, 2) + Math.Pow(model.Y, 2) + Math.Pow(model.Z, 2));
        }

        /// <summary>
        /// Get the ecliptic longitude 'λ'
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static double GetEclipticLongitude(this LocationModel model)
        {
            return DegreeHelper.RadiansToDegrees(Math.Atan2(model.Y, model.X));
        }

        /// <summary>
        /// Get the ecliptic latitude 'β'
        /// </summary>
        /// <param name="model"></param>
        /// <param name="largeDelta"></param>
        /// <returns></returns>
        public static double GetEclipticLatitude(this LocationModel model, double largeDelta)
        {
            return DegreeHelper.RadiansToDegrees(Math.Asin(model.Z / largeDelta));
        }

        public static double GetOrbitRadius(this LocationModel model)
        {
            return Math.Sqrt(Math.Pow(model.X, 2) + Math.Pow(model.Y, 2));
        }

        /// <summary>
        /// Get angle ε
        /// </summary>
        /// <returns></returns>
        public static double GetAngle()
        {
            return 23.4397;
        }

        /// <summary>
        /// Get the declination δ
        /// </summary>
        /// <param name="eclipticLatitude"></param>
        /// <param name="eclipticLongitude"></param>
        /// <param name="angle"></param>
        /// <returns></returns>
        public static double GetDeclination(double eclipticLatitude, double eclipticLongitude, double angle)
        {
            return DegreeHelper.RadiansToDegrees(Math.Asin(
                (MathDegrees.Sin(eclipticLatitude) * MathDegrees.Cos(angle)) +
                (MathDegrees.Cos(eclipticLatitude) * MathDegrees.Sin(angle) * MathDegrees.Sin(eclipticLongitude))));
        }

        /// <summary>
        /// Get the right ascension 'α'
        /// </summary>
        /// <param name="eclipticLatitude"></param>
        /// <param name="eclipticLongitude"></param>
        /// <param name="angle"></param>
        /// <returns></returns>
        public static double GetRightAscension(double eclipticLatitude, double eclipticLongitude, double angle)
        {
            return DegreeHelper.RadiansToDegrees(Math.Atan2(
                (MathDegrees.Sin(eclipticLongitude) * MathDegrees.Cos(angle)) -
                (MathDegrees.Tan(eclipticLatitude) * MathDegrees.Sin(angle)),
                MathDegrees.Cos(eclipticLongitude)));
        }
    }
}