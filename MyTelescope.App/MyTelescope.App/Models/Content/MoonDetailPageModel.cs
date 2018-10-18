namespace MyTelescope.App.Pages.Content
{
    using MyTelescope.Data.Loader.Interfaces;
    using Models.Base;
    using SolarSystem.Models.CelestialObject;
    using ViewModels.Models.Item;

    public class MoonDetailPageModel : DetailOverViewPageModel<MoonDetailViewModel, CelestialObject>
    {
        public MoonDetailPageModel(IStaticDataLoader<MoonDetailViewModel, CelestialObject> dataLoader)
            : base(dataLoader)
        {
        }
    }
}