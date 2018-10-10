namespace MyTelescope.App.Pages.Content
{
    using DataLayer.Interfaces;
    using Models.Base;
    using SolarSystem.Models.CelestialObject;
    using ViewModels.Models.Item;

    public class PlanetDetailPageModel : DetailOverViewPageModel<PlanetDetailViewModel, CelestialObjectModel>
    {
        public PlanetDetailPageModel(IStaticDataLoader<PlanetDetailViewModel, CelestialObjectModel> dataLoader)
            : base(dataLoader)
        {
        }
    }
}