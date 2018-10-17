namespace MyTelescope.SolarSystem.Models.CelestialObject
{
    using Keplerian;
    using SWE.Model.Interfaces;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Diagnostics;
    using Utilities.Interfaces;
    using Utilities.Models;

    [DebuggerDisplay("{ReferenceDate.ToString()}")]
    public class CelestialObjectPosition : IKey, ICelestialObjectReferenceModel
    {
        [Obsolete("For serialisation")]
        public CelestialObjectPosition()
        {
        }

        public CelestialObjectPosition(
            Guid celestialObjectId,
            DateTimeOffset referenceDate,
            DateTimeOffset referenceEndDate)
            : this(celestialObjectId, referenceDate)
        {
            ReferenceEndDate = referenceEndDate;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CelestialObjectPosition"/> class.
        /// For Earth
        /// </summary>
        /// <param name="celestialObjectId"></param>
        /// <param name="referenceDate"></param>
        /// <param name="location"></param>
        /// <param name="averageCentricDistance"></param>
        /// <param name="centricDistance"></param>
        public CelestialObjectPosition(
            Guid celestialObjectId,
            DateTimeOffset referenceDate,
            LocationModel location,
            double averageCentricDistance,
            double centricDistance)
            : this(
                celestialObjectId,
                referenceDate,
                location,
                averageCentricDistance,
                centricDistance,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0)
        {
        }

        public CelestialObjectPosition(
            Guid celestialObjectId,
            DateTimeOffset referenceDate,
            LocationModel location,
            double averageCentricDistance,
            double centricDistance,
            double largeDeltaEarthDistance,
            double eclipticLongitude,
            double eclipticLatitude,
            double declination,
            double rightAscension,
            double ratioSunEarthDistance,
            double ratioEarthAuDistance,
            double meanAnomaly)
        : this(
            celestialObjectId,
            referenceDate,
            location?.X ?? 0,
            location?.Y ?? 0,
            location?.Z ?? 0,
            averageCentricDistance,
            centricDistance,
            largeDeltaEarthDistance,
            eclipticLongitude,
            eclipticLatitude,
            declination,
            rightAscension,
            ratioSunEarthDistance,
            ratioEarthAuDistance,
            meanAnomaly)
        {
        }

        public CelestialObjectPosition(
            Guid celestialObjectId,
            DateTimeOffset referenceDate,
            double x,
            double y,
            double z,
            double averageCentricDistance,
            double centricDistance,
            double largeDeltaEarthDistance,
            double eclipticLongitude,
            double eclipticLatitude,
            double declination,
            double rightAscension,
            double ratioSunEarthDistance,
            double ratioEarthAuDistance,
            double meanAnomaly)
        {
            Id = Guid.NewGuid();
            CelestialObjectId = celestialObjectId;
            ReferenceDate = referenceDate;

            X = x;
            Y = y;
            Z = z;

            LargeDeltaEarthDistance = largeDeltaEarthDistance;
            AverageCentricDistance = averageCentricDistance;
            CentricDistance = centricDistance;

            EclipticLongitude = eclipticLongitude;
            EclipticLatitude = eclipticLatitude;
            Declination = declination;
            RightAscension = rightAscension;
            RatioSunEarthDistance = ratioSunEarthDistance;
            RatioEarthAuDistance = ratioEarthAuDistance;
            MeanAnomaly = meanAnomaly;
        }

        public CelestialObjectPosition(
            Guid celestialObjectId,
            DateTimeOffset referenceDate,
            KeplerianCalculationModel calculationModel)
        : this(
            celestialObjectId,
            referenceDate,
            calculationModel.Location,
            calculationModel.AverageCentricDistance,
            calculationModel.CentricDistance,
            calculationModel.LargeDelta,
            calculationModel.EclipticLongitude,
            calculationModel.EclipticLatitude,
            calculationModel.Declination,
            calculationModel.RightAscension,
            calculationModel.RatioSunEarthDistance,
            calculationModel.RatioEarthAuDistance,
            calculationModel.MeanAnomaly.Degrees)
        {
            Id = Guid.NewGuid();
            CelestialObjectId = celestialObjectId;
            ReferenceDate = referenceDate;
        }

        public CelestialObjectPosition(Guid celestialObjectId, DateTimeOffset referenceDate)
            : this(
                celestialObjectId,
                referenceDate,
                null,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0)
        {
        }

        /// <summary>
        /// Random guid given on creation
        /// </summary>
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }

        [Required]
        public Guid CelestialObjectId { get; set; }

        [Required]
        [ForeignKey(nameof(CelestialObjectId))]
        public CelestialObject CelestialObject { get; set; }

        [Required]
        //[Column(TypeName = "date")]
        public DateTimeOffset ReferenceDate { get; set; }

        [NotMapped]
        public DateTimeOffset? ReferenceEndDate { get; set; }

        /// <summary>
        /// X coördinate in astronomic units
        /// </summary>
        [Required]
        public double X { get; set; }

        /// <summary>
        /// Y coördinate in astronomic units
        /// </summary>
        [Required]
        public double Y { get; set; }

        /// <summary>
        /// Z coördinate in astronomic units
        /// </summary>
        [Required]
        public double Z { get; set; }

        /// <summary>
        /// the Average distance from the centre in AU
        /// </summary>
        public double AverageCentricDistance { get; set; }

        /// <summary>
        /// Distance from the centre in AU
        /// </summary>
        public double CentricDistance { get; set; }

        /// <summary>
        /// Large delta 'Δ'
        /// Distance from the earth in AU
        /// </summary>
        /// <returns></returns>
        public double LargeDeltaEarthDistance { get; set; }

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
        /// the Mean anomaly 'M0'
        /// </summary>
        public double MeanAnomaly { get; set; }

        public LocationModel Location => new LocationModel(X, Y, Z);
    }
}