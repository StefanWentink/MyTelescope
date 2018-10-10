namespace MyTelescope.App.ViewModels.Helpers.Sort
{
    using MyTelescope.Utilities.Models.Sort;
    using SolarSystem.Models.CelestialObject;

    public static class CelestialObjectPositionSortHelper
    {
        internal static SortModel GetDefaultSort()
        {
            return new SortModel($"{nameof(CelestialObjectPositionModel.AverageCentricDistance)}", true);
        }
    }
}
