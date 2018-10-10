namespace MyTelescope.SolarSystem.Models.CelestialObject
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Diagnostics;
    using Interfaces;
    using Utilities.Interfaces;

    [DebuggerDisplay("{" + nameof(Code) + "}")]
    public class CelestialObjectModel : IKeyModel, ICodeModel, ICelestialObjectTypeReferenceModel, ICalculatableRadius
    {
        [Obsolete("For serialisation")]
        public CelestialObjectModel()
        {
            InitCollections();
        }

        public CelestialObjectModel(
            string code,
            Guid celestialObjectTypeId,
            string colorCode,
            string imageUrl,
            double mass,
            double siderealOrbitPeriod,
            double siderealRotationPeriod,
            double meanDensity,
            double equatorialRadius,
            double polarRadius,
            double semiMajorAxis,
            Guid? celestialObjectId)
        {
            Id = Guid.NewGuid();
            Code = code;
            ImageUrl = imageUrl;
            ColorCode = colorCode;
            CelestialObjectTypeId = celestialObjectTypeId;
            Mass = mass;
            SiderealOrbitPeriod = siderealOrbitPeriod;
            SiderealRotationPeriod = siderealRotationPeriod;
            MeanDensity = meanDensity;
            EquatorialRadius = equatorialRadius;
            PolarRadius = polarRadius;
            CelestialObjectId = celestialObjectId;
            SemiMajorAxis = semiMajorAxis;
            InitCollections();
        }

        private void InitCollections()
        {
            CelestialObjectPositions = new HashSet<CelestialObjectPositionModel>();
        }

        public CelestialObjectModel(
            string code,
            bool inferiorOrbit,
            Guid celestialObjectTypeId,
            Guid? celestialObjectId,
            string colorCode,
            string imageUrl,
            double mass,
            double volume,
            double equatorialRadius,
            double polarRadius,
            double siderealOrbitPeriod,
            double siderealRotationPeriod,
            double lengthOfDay,
            double synodicPeriod,
            double semiMajorAxis,
            double orbitalEccentricity,
            double perihelion,
            double aphelion,
            double meanDensity,
            double gravity,
            double surfaceAcceleration,
            double escapeVelocity,
            double solarIrradiance,
            double blackBodyTemperature,
            double topographicRange,
            int satellites,
            bool ringSystem,
            double minimumDistance,
            double oppositionDistance,
            double maximumDistance,
            double maximumApparentDiameter,
            double minimumApparentDiameter,
            double maximumVisualMagnitude)
        : this(
            code,
            celestialObjectTypeId,
            colorCode,
            imageUrl,
            mass,
            siderealOrbitPeriod,
            siderealRotationPeriod,
            meanDensity,
            equatorialRadius,
            polarRadius,
            semiMajorAxis,
            celestialObjectId)
        {
            InferiorOrbit = inferiorOrbit;
            Volume = volume;
            OrbitalEccentricity = orbitalEccentricity;
            Perihelion = perihelion;
            Aphelion = aphelion;
            SynodicPeriod = synodicPeriod;
            LengthOfDay = lengthOfDay;

            Gravity = gravity;
            SurfaceAcceleration = surfaceAcceleration;
            EscapeVelocity = escapeVelocity;
            SolarIrradiance = solarIrradiance;
            BlackBodyTemperature = blackBodyTemperature;
            TopographicRange = topographicRange;
            Satellites = satellites;
            RingSystem = ringSystem;
            MinimumDistance = minimumDistance;
            OppositionDistance = oppositionDistance;
            MaximumDistance = maximumDistance;
            MaximumApparentDiameter = maximumApparentDiameter;
            MinimumApparentDiameter = minimumApparentDiameter;
            MaximumVisualMagnitude = maximumVisualMagnitude;
        }

        /// <summary>
        /// Random guid given on creation
        /// </summary>
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }

        [ForeignKey(nameof(CelestialObjectId))]
        public CelestialObjectModel CelestialObject { get; set; }

        public Guid? CelestialObjectId { get; set; }

        [Required]
        public Guid CelestialObjectTypeId { get; set; }

        [Required]
        [ForeignKey(nameof(CelestialObjectTypeId))]
        public CelestialObjectTypeModel CelestialObjectType { get; set; }

        [Required]
        public string Code { get; set; }

        [Required]
        public string ColorCode { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        public virtual ICollection<CelestialObjectPositionModel> CelestialObjectPositions { get; set; }

        /// <summary>
        /// Inferior orbit
        /// </summary>
        public bool InferiorOrbit { get; set; }

        /// <summary>
        /// Mass(10^24 kg)
        /// </summary>
        public double Mass { get; set; }

        /// <summary>
        /// Volume(10^10 km3)
        /// </summary>
        public double Volume { get; set; }

        /// <summary>
        /// Equatorial radius(km)
        /// </summary>
        public double EquatorialRadius { get; set; }

        /// <summary>
        /// Polar radius(km)
        /// </summary>
        public double PolarRadius { get; set; }

        /// <summary>
        /// Semi-Major Axis(km)
        /// </summary>
        public double SemiMajorAxis { get; set; }

        /// <summary>
        /// Orbital eccentricity
        /// </summary>
        public double OrbitalEccentricity { get; set; }

        /// <summary>
        /// Sidereal orbit period Axis(days)
        /// </summary>
        public double SiderealOrbitPeriod { get; set; }

        /// <summary>
        /// Sidereal rotation period Axis(days)
        /// </summary>
        public double SiderealRotationPeriod { get; set; }

        /// <summary>
        /// Length of day(hours)
        /// </summary>
        public double LengthOfDay { get; set; }
        
        /// <summary>
        /// Synodic period(days)
        /// </summary>
        public double SynodicPeriod { get; set; }

        /// <summary>
        /// Perihelion (10^6 km)
        /// </summary>
        public double Perihelion { get; set; }

        /// <summary>
        /// Aphelion (10^6 km)
        /// </summary>
        public double Aphelion { get; set; }

        /// <summary>
        /// Mean density(kg/m3)
        /// </summary>
        public double MeanDensity { get; set; }

        /// <summary>
        /// Surface gravity(eq.) (m/s2)
        /// </summary>
        public double Gravity { get; set; }

        /// <summary>
        /// Surface acceleration(eq.) (m/s2)
        /// </summary>
        public double SurfaceAcceleration { get; set; }

        /// <summary>
        /// Escape velocity(km/s)
        /// </summary>
        public double EscapeVelocity { get; set; }

        /// <summary>
        /// Solar irradiance(W/m2)
        /// </summary>
        public double SolarIrradiance { get; set; }

        /// <summary>
        /// Black-body temperature(K)
        /// </summary>
        public double BlackBodyTemperature { get; set; }

        /// <summary>
        /// Topographic range(km)
        /// </summary>
        public double TopographicRange { get; set; }

        /// <summary>
        /// Number of natural satellites
        /// </summary>
        public int Satellites { get; set; }

        /// <summary>
        /// Planetary ring system
        /// </summary>
        public bool RingSystem { get; set; }

        /// <summary>
        /// Distance from Earth - Minimum(10 ^ 6 km)
        /// </summary>
        public double MinimumDistance { get; set; }

        /// <summary>
        /// Distance from Earth - Opposition(10 ^ 6 km)
        /// </summary>
        public double OppositionDistance { get; set; }

        /// <summary>
        /// Distance from Earth - Maximum(10 ^ 6 km)
        /// </summary>
        public double MaximumDistance { get; set; }

        /// <summary>
        /// Apparent diameter - Maximum(seconds of arc)
        /// </summary>
        public double MaximumApparentDiameter { get; set; }

        /// <summary>
        /// Apparent diameter - Minimum(seconds of arc)
        /// </summary>
        public double MinimumApparentDiameter { get; set; }

        /// <summary>
        /// Maximum visual magnitude  
        /// </summary>
        public double MaximumVisualMagnitude { get; set; }
    }
}
