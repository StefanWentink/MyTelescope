namespace MyTelescope.App.ViewModels.Models.Item
{
    using System.ComponentModel.DataAnnotations;
    using MyTelescope.Utilities.Helpers;
    using SolarSystem.Enums;
    using SolarSystem.Extensions;
    using SolarSystem.Interfaces;
    using SolarSystem.Models.CelestialObject;

    public abstract class CelestialObjectDetailViewModel : BaseKeyDetailViewModel<CelestialObjectModel>, ICalculatableRadius
    {
        protected CelestialObjectDetailViewModel(CelestialObjectModel model)
            : base(model)
        {
        }

        public abstract CelestialObjectType CelestialObjectType { get; }

        [Required]
        public string Code
        {
            get => Model.Code;
            set => Model.Code = value;
        }

        /// <summary>
        /// Inferior orbit
        /// </summary>
        public bool InferiorOrbit
        {
            get => Model.InferiorOrbit;
            set => Model.InferiorOrbit = value;
        }

        /// <summary>
        /// Mass(10^24 kg)
        /// </summary>
        public double Mass
        {
            get => Model.Mass;
            set => Model.Mass = value;
        }

        /// <summary>
        /// Volume(10^10 km3)
        /// </summary>
        public double Volume
        {
            get => Model.Volume;
            set => Model.Volume = value;
        }

        /// <summary>
        /// Equatorial radius(km)
        /// </summary>
        public double EquatorialRadius
        {
            get => Model.EquatorialRadius;
            set => Model.EquatorialRadius = value;
        }

        /// <summary>
        /// Polar radius(km)
        /// </summary>
        public double PolarRadius
        {
            get => Model.PolarRadius;
            set => Model.PolarRadius = value;
        }

        /// <summary>
        /// Volumetric mean radius(km)
        /// </summary>
        public double VolumetricMeanRadius => this.GetVolumetricMeanRadius();

        /// <summary>
        /// Ellipticity (Flattening)
        /// </summary>
        public double Ellipticity => this.GetEllipticity();

        /// <summary>
        /// Semi-Major Axis(10^6 km)
        /// </summary>
        public double SemiMajorAxis
        {
            get => Model.SemiMajorAxis;
            set => Model.SemiMajorAxis = value;
        }

        /// <summary>
        /// Sidereal rotation period Axis(days)
        /// </summary>
        public double SiderealRotationPeriod
        {
            get => Model.SiderealRotationPeriod;
            set => Model.SiderealRotationPeriod = value;
        }

        /// <summary>
        /// Sidereal orbit period Axis(days)
        /// </summary>
        public double SiderealOrbitPeriod
        {
            get => Model.SiderealOrbitPeriod;
            set => Model.SiderealOrbitPeriod = value;
        }

        /// <summary>
        /// Perihelion (10^6 km)
        /// </summary>
        public double Perihelion
        {
            get => Model.Perihelion;
            set => Model.Perihelion = value;
        }

        /// <summary>
        /// Aphelion (10^6 km)
        /// </summary>
        public double Aphelion
        {
            get => Model.Aphelion;
            set => Model.Aphelion = value;
        }

        /// <summary>
        /// SynodicPeriod (days)
        /// </summary>
        public double SynodicPeriod
        {
            get => Model.SynodicPeriod;
            set => Model.SynodicPeriod = value;
        }

        /// <summary>
        /// LengthOfDay (hours)
        /// </summary>
        public double LengthOfDay
        {
            get => Model.LengthOfDay;
            set => Model.LengthOfDay = value;
        }

        /// <summary>
        /// Mean density(kg/m3)
        /// </summary>
        public double MeanDensity
        {
            get => Model.MeanDensity;
            set => Model.MeanDensity = value;
        }

        /// <summary>
        /// Surface gravity(eq.) (m/s2)
        /// </summary>
        public double Gravity
        {
            get => Model.Gravity;
            set => Model.Gravity = value;
        }

        /// <summary>
        /// Surface acceleration(eq.) (m/s2)
        /// </summary>
        public double SurfaceAcceleration
        {
            get => Model.SurfaceAcceleration;
            set => Model.SurfaceAcceleration = value;
        }

        /// <summary>
        /// Escape velocity(km/s)
        /// </summary>
        public double EscapeVelocity
        {
            get => Model.EscapeVelocity;
            set => Model.EscapeVelocity = value;
        }

        /// <summary>
        /// Solar irradiance(W/m2)
        /// </summary>
        public double SolarIrradiance
        {
            get => Model.SolarIrradiance;
            set => Model.SolarIrradiance = value;
        }

        /// <summary>
        /// Black-body temperature(K)
        /// </summary>
        public double BlackBodyTemperature
        {
            get => Model.BlackBodyTemperature;
            set => Model.BlackBodyTemperature = value;
        }

        /// <summary>
        /// Topographic range(km)
        /// </summary>
        public double TopographicRange
        {
            get => Model.TopographicRange;
            set => Model.TopographicRange = value;
        }

        /// <summary>
        /// Number of natural satellites
        /// </summary>
        public int Satellites
        {
            get => Model.Satellites;
            set => Model.Satellites = value;
        }

        /// <summary>
        /// Planetary ring system
        /// </summary>
        public bool RingSystem
        {
            get => Model.RingSystem;
            set => Model.RingSystem = value;
        }

        /// <summary>
        /// Distance from Earth - Minimum(10 ^ 6 km)
        /// </summary>
        public double MinimumDistance
        {
            get => Model.MinimumDistance;
            set => Model.MinimumDistance = value;
        }

        /// <summary>
        /// Distance from Earth - Opposition(10 ^ 6 km)
        /// </summary>
        public double OppositionDistance
        {
            get => Model.OppositionDistance;
            set => Model.OppositionDistance = value;
        }

        /// <summary>
        /// Distance from Earth - Maximum(10 ^ 6 km)
        /// </summary>
        public double MaximumDistance
        {
            get => Model.MaximumDistance;
            set => Model.MaximumDistance = value;
        }

        /// <summary>
        /// Apparent diameter - Maximum(seconds of arc)
        /// </summary>
        public double MaximumApparentDiameter
        {
            get => Model.MaximumApparentDiameter;
            set => Model.MaximumApparentDiameter = value;
        }

        /// <summary>
        /// Apparent diameter - Minimum(seconds of arc)
        /// </summary>
        public double MinimumApparentDiameter
        {
            get => Model.MinimumApparentDiameter;
            set => Model.MinimumApparentDiameter = value;
        }

        /// <summary>
        /// Maximum visual magnitude  
        /// </summary>
        public double MaximumVisualMagnitude
        {
            get => Model.MaximumVisualMagnitude;
            set => Model.MaximumVisualMagnitude = value;
        }
    }
}