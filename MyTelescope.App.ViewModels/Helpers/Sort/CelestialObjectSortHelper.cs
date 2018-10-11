namespace MyTelescope.App.ViewModels.Helpers.Sort
{
    using MyTelescope.Utilities.Models.Sort;
    using SolarSystem.Models.CelestialObject;

    public static class CelestialObjectSortHelper
    {
        internal static SortModel GetDefaultSort()
        {
            return new SortModel(nameof(CelestialObjectModel.MaximumDistance), true);
        }
    }
}