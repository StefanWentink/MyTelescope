namespace MyTelescope.App.ViewModels.Models.Item
{
    using Enums;
    using Interfaces;
    using Localisation.Resources.MyTelescope;
    using SolarSystem.Enums;
    using SolarSystem.Models.CelestialObject;
    using System.Collections.Generic;

    public class PlanetViewModel : CelestialObjectDetailViewModel
    {
        public PlanetViewModel(CelestialObjectModel model)
            : base(model)
        {
        }

        public override CelestialObjectType CelestialObjectType => CelestialObjectType.Planet;

        public override List<IDetailViewModel> GetDetails()
        {
            return new List<IDetailViewModel>
            {
                new UnitValueDetailViewModel<double>(
                TextResource.Mass,
                Mass,
                $"{TextResource.Pow24}({TextResource.Pow24Number}) {TextResource.KilogramShort}",
                DisplayType.Mass).GetDetailView(),

                new UnitValueDetailViewModel<double>(
                TextResource.Volume,
                Volume * 10,
                $"{TextResource.Pow9}({TextResource.Pow9Number}) {TextResource.CubicKilometerShort}",
                DisplayType.Volume).GetDetailView(),

                new UnitValueDetailViewModel<double>(
                TextResource.VolumetricMeanRadius,
                VolumetricMeanRadius,
                TextResource.KilometerShort,
                DisplayType.Distance).GetDetailView(),

                new UnitValueDetailViewModel<double>(
                TextResource.Ellipticity,
                Ellipticity * 100,
                TextResource.PercentageShort,
                DisplayType.Percentage).GetDetailView(),

                new UnitValueDetailViewModel<int>(
                TextResource.Satellites,
                Satellites,
                string.Empty,
                DisplayType.Int).GetDetailView(),

                new UnitValueDetailViewModel<bool>(
                TextResource.RingSystem,
                RingSystem,
                string.Empty,
                DisplayType.Bool).GetDetailView(),

                new UnitValueDetailViewModel<double>(
                TextResource.BlackBodyTemperature,
                BlackBodyTemperature,
                $"{TextResource.DegreeShort} {TextResource.Kelvin}",
                DisplayType.Temperature).GetDetailView(),

                new UnitValueDetailViewModel<double>(
                TextResource.Gravity,
                Gravity,
                $"{TextResource.Acceleration} ({TextResource.MS2})",
                DisplayType.Speed).GetDetailView(),

                new UnitValueDetailViewModel<double>(
                TextResource.MeanDensity,
                MeanDensity,
                $"{TextResource.Density} ({TextResource.KgPerM3})",
                DisplayType.Density).GetDetailView(),

                new UnitValueDetailViewModel<double>(
                $"{TextResource.Minimum} {TextResource.Earth} {TextResource.Distance.ToLowerInvariant()}",
                MinimumDistance,
                $"{TextResource.Million} {TextResource.KilometerShort}",
                DisplayType.Density).GetDetailView(),

                new UnitValueDetailViewModel<double>(
                $"{TextResource.Maximum} {TextResource.Earth} {TextResource.Distance.ToLowerInvariant()}",
                MaximumDistance,
                $"{TextResource.Million} {TextResource.KilometerShort}",
                DisplayType.Distance).GetDetailView(),

                new UnitValueDetailViewModel<double>(
                $"{TextResource.Average} {TextResource.Sun} {TextResource.Distance.ToLowerInvariant()}",
                SemiMajorAxis,
                $"{TextResource.Million} {TextResource.KilometerShort}",
                DisplayType.Distance).GetDetailView(),

                new UnitValueDetailViewModel<double>(
                $"{TextResource.Perihelion} ({TextResource.AphelionDescription.ToLowerInvariant()})",
                Perihelion,
                $"{TextResource.Million} {TextResource.KilometerShort}",
                DisplayType.Distance).GetDetailView(),

                new UnitValueDetailViewModel<double>(
                $"{TextResource.Aphelion} ({TextResource.AphelionDescription.ToLowerInvariant()})",
                Aphelion,
                $"{TextResource.Aphelion} {TextResource.KilometerShort}",
                DisplayType.Distance).GetDetailView(),

                new UnitValueDetailViewModel<double>(
                TextResource.SiderealRotationPeriod,
                SiderealRotationPeriod,
                TextResource.Hour,
                DisplayType.Time).GetDetailView(),

                new UnitValueDetailViewModel<double>(
                TextResource.SiderealOrbitPeriod,
                SiderealOrbitPeriod,
                TextResource.Day,
                DisplayType.Distance).GetDetailView(),

                new UnitValueDetailViewModel<double>(
                TextResource.LengthOfDay,
                LengthOfDay,
                TextResource.Hour,
                DisplayType.Time).GetDetailView(),

                new UnitValueDetailViewModel<double>(
                TextResource.SynodicPeriod,
                SynodicPeriod,
                TextResource.Day,
                DisplayType.Time).GetDetailView(),

                new UnitValueDetailViewModel<double>(
                $"{TextResource.Minimum} {TextResource.ApparentDiameter.ToLowerInvariant()}",
                MinimumApparentDiameter,
                $"{TextResource.ArcSecondShort} {TextResource.ArcSecond}",
                DisplayType.Density).GetDetailView(),

                new UnitValueDetailViewModel<double>(
                $"{TextResource.Maximum} {TextResource.ApparentDiameter.ToLowerInvariant()}",
                MaximumApparentDiameter,
                $"{TextResource.ArcSecondShort} {TextResource.ArcSecond}",
                DisplayType.Distance).GetDetailView(),

                new UnitValueDetailViewModel<double>(
                $"{TextResource.Maximum} {TextResource.VisualMagnitude.ToLowerInvariant()}",
                MaximumVisualMagnitude,
                string.Empty,
                DisplayType.Double).GetDetailView()
            };
        }
    }
}