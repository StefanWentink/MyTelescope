namespace MyTelescope.App.Pages.Content
{
    using DataLayer.Interfaces;
    using Models.Base;
    using SolarSystem.Models.CelestialObject;
    using ViewModels.Models.Item;

    public class MoonDetailPageModel : DetailOverViewPageModel<MoonDetailViewModel, CelestialObjectModel>
    {
        public MoonDetailPageModel(IStaticDataLoader<MoonDetailViewModel, CelestialObjectModel> dataLoader)
            : base(dataLoader)
        {
        }
    }
}