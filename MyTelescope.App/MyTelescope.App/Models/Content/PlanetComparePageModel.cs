namespace MyTelescope.App.Pages.Content
{
    using MyTelescope.Data.Loader.Interfaces;
    using Models.Base;

    using MyTelescope.Utilities.Helpers;
    using SolarSystem.Models.CelestialObject;
    using ViewModels.Models.Item;

    public class PlanetComparePageModel : CelestialComparePageModel
    {
        public override string CanvasViewKey => ModelHelper.GetName(GetType().Name);

        public PlanetComparePageModel(IHttpDataLoader<CelestialObjectViewModel, CelestialObject> dataLoader)
            : base(dataLoader)
        {
        }
    }
}