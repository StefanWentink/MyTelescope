namespace MyTelescope.App.ViewModels.Models.Item
{
    using Enums;
    using Interfaces;
    using Localisation.Resources.MyTelescope;
    using SolarSystem.Enums;
    using SolarSystem.Models.CelestialObject;
    using System;
    using System.Collections.Generic;

    public class MoonViewModel : CelestialObjectDetailViewModel
    {
        public MoonViewModel(CelestialObject model)
            : base(model)
        {
        }

        public override CelestialType CelestialObjectType => CelestialType.MajorMoon;

        public override List<IDetailViewModel> GetDetails()
        {
            return new List<IDetailViewModel>
            {
                new UnitValueDetailViewModel<double>(
                TextResource.Mass,
                Mass * Math.Pow(10, 6),
                $"{TextResource.Pow18}({TextResource.Pow18Number}) {TextResource.KilogramShort}",
                DisplayType.Mass).GetDetailView(),

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

                new UnitValueDetailViewModel<double>(
                TextResource.MeanDensity,
                MeanDensity,
                $"{TextResource.Density} ({TextResource.KgPerM3})",
                DisplayType.Density).GetDetailView(),

                new UnitValueDetailViewModel<double>(
                $"{TextResource.Average} {TextResource.Planet} {TextResource.Distance.ToLowerInvariant()}",
                SemiMajorAxis * Math.Pow(10, 6),
                TextResource.Kilometer,
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
                DisplayType.Distance).GetDetailView()
            };
        }
    }
}