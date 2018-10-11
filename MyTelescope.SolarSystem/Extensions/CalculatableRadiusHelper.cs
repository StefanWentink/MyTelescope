namespace MyTelescope.SolarSystem.Extensions
{
    using Interfaces;
    using Utilities.Helpers;

    public static class CalculatableRadiusHelper
    {
        public static double GetVolumetricMeanRadius(this ICalculatableRadius model) =>
            (model.EquatorialRadius + model.PolarRadius) / 2;

        public static double GetEllipticity(this ICalculatableRadius model) =>
             1 - (DoubleHelper.GetMin(model.EquatorialRadius, model.PolarRadius) / DoubleHelper.GetMax(model.EquatorialRadius, model.PolarRadius));
    }
}