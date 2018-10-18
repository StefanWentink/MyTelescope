namespace MyTelescope.App.Pages.Content
{
    using MyTelescope.Data.Loader.Interfaces;
    using Models.Base;
    using SolarSystem.Models.CelestialObject;
    using ViewModels.Models.Item;

    public class PlanetDetailPageModel : DetailOverViewPageModel<PlanetDetailViewModel, CelestialObject>
    {
        public PlanetDetailPageModel(IStaticDataLoader<PlanetDetailViewModel, CelestialObject> dataLoader)
            : base(dataLoader)
        {
        }
    }
}