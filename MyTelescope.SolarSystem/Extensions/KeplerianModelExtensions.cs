namespace MyTelescope.SolarSystem.Extensions
{
    using System;
    using Models.Keplerian;

    public static class KeplerianModelExtensions
    {
        public static KeplerianDateModel GetKeplerianDateModel(this KeplerianModel model, DateTimeOffset referenceDate)
        {
            return new KeplerianDateModel(referenceDate, model);
        }

        public static KeplerianValueModel GetKeplerianValueModel(this KeplerianModel model, double j2000CenturyFactor)
        {
            return new KeplerianValueModel(
                model.BaseValues.SemiMajorAxis.Degrees + (model.CenturyValues.SemiMajorAxis.Degrees * j2000CenturyFactor),
                model.BaseValues.Eccentricity + (model.CenturyValues.Eccentricity * j2000CenturyFactor),
                model.BaseValues.Inclination.Degrees + (model.CenturyValues.Inclination.Degrees * j2000CenturyFactor),
                model.BaseValues.MeanLongitude.Degrees + (model.CenturyValues.MeanLongitude.Degrees * j2000CenturyFactor),
                model.BaseValues.PerihelionLongitude.Degrees + (model.CenturyValues.PerihelionLongitude.Degrees * j2000CenturyFactor),
                model.BaseValues.AscendingNodeLongitude.Degrees + (model.CenturyValues.AscendingNodeLongitude.Degrees * j2000CenturyFactor));
        }
    }
}