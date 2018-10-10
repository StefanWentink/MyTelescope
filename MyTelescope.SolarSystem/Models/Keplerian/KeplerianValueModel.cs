namespace MyTelescope.SolarSystem.Models.Keplerian
{
    using Utilities.Helpers;
    using Utilities.Models;

    public class KeplerianValueModel
    {
        /// <summary>
        /// 'a' - SemiMajorAxis
        /// </summary>
        public DegreeModel SemiMajorAxis { get; }

        /// <summary>
        /// 'e' - Eccentricity
        /// </summary>
        public double Eccentricity { get; }

        /// <summary>
        /// 'i' - Inclination
        /// </summary>
        public DegreeModel Inclination { get; }

        /// <summary>
        /// 'L' - MeanLongitude
        /// </summary>
        public DegreeModel MeanLongitude { get; }

        /// <summary>
        /// 'ϖ' - PerihelionLongitude
        /// </summary>
        public DegreeModel PerihelionLongitude { get; }

        /// <summary>
        /// 'Ω' - AscendingNodeLongitude
        /// </summary>
        public DegreeModel AscendingNodeLongitude { get; }
        
        public KeplerianValueModel(
            double semiMajorAxis,
            double eccentricity,
            double inclination,
            double meanLongitude,
            double perihelionLongitude,
            double ascendingNodeLongitude)
        {
            SemiMajorAxis = new DegreeModel(semiMajorAxis);
            Eccentricity = eccentricity;
            Inclination = new DegreeModel(inclination);
            MeanLongitude = new DegreeModel(meanLongitude);
            PerihelionLongitude = new DegreeModel(perihelionLongitude);
            AscendingNodeLongitude = new DegreeModel(ascendingNodeLongitude);
        }

        /// <summary>
        /// Get the argument of Perihelion 'ω' = 'ϖ' - 'Ω'
        /// </summary>
        /// <returns></returns>
        public DegreeModel GetArgumentOfPerihelion()
        {
            return new DegreeModel(DegreeHelper.Modulo360Absolute(PerihelionLongitude.Degrees - AscendingNodeLongitude.Degrees));
        }

        /// <summary>
        /// Get the argument of Perihelion 'M' = 'L' - 'ϖ' or 'L' - 'ω'' - 'Ω' 
        /// </summary>
        /// <returns></returns>
        public DegreeModel GetMeanAnomaly360()
        {
            return new DegreeModel(DegreeHelper.Modulo360Absolute(MeanLongitude.Degrees - PerihelionLongitude.Degrees));
        }

        /// <summary>
        /// Get the argument of Perihelion 'M' = 'L' - 'ϖ' or 'L' - 'ω'' - 'Ω' 
        /// </summary>
        /// <returns></returns>
        public DegreeModel GetMeanAnomalyAroundZero()
        {
            return new DegreeModel(DegreeHelper.Modulo360AroundZero(MeanLongitude.Degrees - PerihelionLongitude.Degrees));
        }
    }
}