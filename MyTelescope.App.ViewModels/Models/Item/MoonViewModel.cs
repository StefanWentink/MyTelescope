namespace MyTelescope.App.ViewModels.Models.Item
{
    using System;
    using System.Collections.Generic;
    using Enums;
    using Interfaces;
    using Localisation.Resources.MyTelescope;
    using SolarSystem.Enums;
    using SolarSystem.Models.CelestialObject;

    public class MoonViewModel : CelestialObjectDetailViewModel
    {
        public MoonViewModel(CelestialObjectModel model)
            : base(model)
        {
        }

        public override CelestialObjectType CelestialObjectType => CelestialObjectType.MajorMoon;

        public override List<IDetailViewModel> GetDetails()
        {
            var result = new List<IDetailViewModel>();

            result.Add(new UnitValueDetailViewModel<double>(
                TextResource.Mass,
                Mass * Math.Pow(10, 6),
                $"{TextResource.Pow18}({TextResource.Pow18Number}) {TextResource.KilogramShort}",
                DisplayType.Mass).GetDetailView());

            result.Add(new UnitValueDetailViewModel<double>(
                TextResource.VolumetricMeanRadius,
                VolumetricMeanRadius,
                TextResource.KilometerShort,
                DisplayType.Distance).GetDetailView());

            result.Add(new UnitValueDetailViewModel<double>(
                TextResource.Ellipticity,
                Ellipticity * 100,
                TextResource.PercentageShort,
                DisplayType.Percentage).GetDetailView());

            result.Add(new UnitValueDetailViewModel<double>(
                TextResource.MeanDensity,
                MeanDensity,
                $"{TextResource.Density} ({TextResource.KgPerM3})",
                DisplayType.Density).GetDetailView());

            result.Add(new UnitValueDetailViewModel<double>(
                $"{TextResource.Average} {TextResource.Planet} {TextResource.Distance.ToLowerInvariant()}",
                SemiMajorAxis * Math.Pow(10, 6),
                $"{TextResource.Kilometer}",
                DisplayType.Distance).GetDetailView());

            result.Add(new UnitValueDetailViewModel<double>(
                $"{TextResource.SiderealRotationPeriod}",
                SiderealRotationPeriod,
                $"{TextResource.Hour}",
                DisplayType.Time).GetDetailView());

            result.Add(new UnitValueDetailViewModel<double>(
                $"{TextResource.SiderealOrbitPeriod}",
                SiderealOrbitPeriod,
                $"{TextResource.Day}",
                DisplayType.Distance).GetDetailView());

            return result;
        }
    }
}