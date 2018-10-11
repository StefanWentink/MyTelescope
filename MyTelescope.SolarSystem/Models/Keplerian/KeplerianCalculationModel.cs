namespace MyTelescope.SolarSystem.Models.Keplerian
{
    using Helpers;
    using Utilities.Helpers;
    using Utilities.Models;

    public class KeplerianCalculationModel
    {
        /// <summary>
        /// the Mean anomaly 'M0'
        /// </summary>
        public DegreeModel MeanAnomaly { get; }

        /// <summary>
        /// the Eccentric anomaly 'E'
        /// </summary>
        public DegreeModel EccentricAnomaly { get; }

        /// <summary>
        /// the True anomaly 'ν'
        /// </summary>
        public DegreeModel TrueAnomaly { get; }

        /// <summary>
        /// the Average distance from the centre in AU 'n'
        /// </summary>
        public double AverageCentricDistance { get; }

        /// <summary>
        /// Distance from the centre in AU 'n'
        /// </summary>
        public double CentricDistance { get; }

        /// <summary>
        /// Large delta 'Δ' (earth distance)
        /// </summary>
        /// <returns></returns>
        public double LargeDelta { get; set; }

        /// <summary>
        /// Ecliptic longitude 'λ'
        /// </summary>
        /// <returns></returns>
        public double EclipticLongitude { get; set; }

        /// <summary>
        /// Ecliptic latitude 'β'
        /// </summary>
        /// <returns></returns>
        public double EclipticLatitude { get; set; }

        /// <summary>
        /// Declination δ
        /// </summary>
        /// <returns></returns>
        public double Declination { get; set; }

        /// <summary>
        /// Right ascension 'α'
        /// </summary>
        /// <returns></returns>
        public double RightAscension { get; set; }

        /// <summary>
        /// Ratio Distance to Sun / Distance to earth
        /// Used for outer planet
        /// </summary>
        /// <returns></returns>
        public double RatioSunEarthDistance { get; set; }

        /// <summary>
        /// Ratio distance to earth / Distance to earth-sun
        /// Used for inner planet
        /// </summary>
        /// <returns></returns>
        public double RatioEarthAuDistance { get; set; }

        /// <summary>
        /// Location x y z
        /// </summary>
        public LocationModel Location { get; }

        public KeplerianCalculationModel(KeplerianDateModel model)
        {
            MeanAnomaly = model.Values.GetMeanAnomaly360();
            EccentricAnomaly = new DegreeModel(KeplerianHelper.GetEccentricAnomaly(model.Values.Eccentricity, MeanAnomaly));
            TrueAnomaly = new DegreeModel(KeplerianHelper.GetEccentricAnomaly2(model.Values.Eccentricity, MeanAnomaly));

            TrueAnomaly = EccentricAnomaly;

            AverageCentricDistance = KeplerianHelper.GetAverageCentricDistance(model.Values.SemiMajorAxis.Degrees, model.Values.Eccentricity);
            CentricDistance = KeplerianHelper.GetCentricDistance(AverageCentricDistance, model.Values.Eccentricity, TrueAnomaly);
            Location = KeplerianHelper.GetLocation(CentricDistance, model.Values.AscendingNodeLongitude, model.Values.GetArgumentOfPerihelion(), TrueAnomaly, model.Values.Inclination);
        }

        public void SetEarthPosition(LocationModel location, double earthCentricDistance)
        {
            var locationdelta = Location.GetLocationDelta(location);
            LargeDelta = locationdelta.GetLargeDelta();

            RatioSunEarthDistance = CentricDistance / LargeDelta;
            RatioEarthAuDistance = LargeDelta / earthCentricDistance;

            EclipticLongitude = locationdelta.GetEclipticLongitude();
            EclipticLatitude = locationdelta.GetEclipticLatitude(LargeDelta);
            Declination = LocationHelper.GetDeclination(EclipticLatitude, EclipticLongitude, LocationHelper.GetAngle());
            RightAscension = LocationHelper.GetRightAscension(EclipticLatitude, EclipticLongitude, LocationHelper.GetAngle());
        }
    }
}