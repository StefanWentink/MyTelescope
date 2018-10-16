namespace MyTelescope.App.Models
{
    using Base;
    using Constants;
    using Localisation.Resources.MyTelescope;
    using Pages.Content;
    using SolarSystem.Models.CelestialObject;

    public class MainPageModel : TabbedPageModel<CelestialObject>
    {
        public MainPageModel(CelestialObject model)
            : base(model, NavigationServiceNameConstants.Main)
        {
        }

        protected override void OnModelPop()
        {
            var positionViewModel = SolarSystem.Helpers.Seeder.CelestialObjectSeedHelper.GetSunPosition();

            AddTab<SolarSystemPageModel>(TextResource.SolarSystem, "icon.png", positionViewModel);
            AddTab<PlanetOverviewPageModel>(TextResource.Planets, "icon.png", Model);
            AddTab<PlanetComparePageModel>(TextResource.Compare, "icon.png", Model);
        }
    }
}