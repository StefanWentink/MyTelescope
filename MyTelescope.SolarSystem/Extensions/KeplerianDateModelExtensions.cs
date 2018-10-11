namespace MyTelescope.SolarSystem.Extensions
{
    using Helpers;
    using Models.Keplerian;

    public static class KeplerianDateModelExtensions
    {
        ///// <summary>
        ///// Compute the angle in degrees per day 'n'
        ///// </summary>
        ///// <param name="model"></param>
        ///// <returns></returns>
        //public static double GetAnglePerDay(this KeplerianDateModel model)
        //{
        //    return KeplerianHelper.GetAnglePerDay(model.Values.SemiMajorAxis);
        //}

        ///// <summary>
        ///// Compute the mean anomaly 'M'
        ///// </summary>
        ///// <param name="model"></param>
        ///// <returns></returns>
        //public static double GetMeanAnomaly(this KeplerianDateModel model)
        //{
        //    return (model.Values.MeanLongitude - model.Values.PerihelionLongitude) % 360;
        //}

        ///// <summary>
        ///// Compute the mean anomaly 'M'
        ///// </summary>
        ///// <param name="model"></param>
        ///// <returns></returns>
        //public static double GetMeanAnomalyForDay(this KeplerianDateModel model)
        //{
        //    return (model.Values.MeanLongitude - model.Values.PerihelionLongitude) % 360;
        //}

        /// <summary>
        /// Compute the true anomaly 'ν'
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static double GetEccentricAnomaly(this KeplerianDateModel model)
        {
            var eccentricity = model.Values.Eccentricity;
            var meanAnomaly = model.Values.GetMeanAnomaly360();
            return KeplerianHelper.GetEccentricAnomaly(eccentricity, meanAnomaly);
        }
    }
}