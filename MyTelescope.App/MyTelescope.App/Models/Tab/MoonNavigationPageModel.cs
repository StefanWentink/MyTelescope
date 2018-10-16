namespace MyTelescope.App.Pages.Tab
{
    using Constants;
    using Content;
    using Models.Base;
    using SolarSystem.Models.CelestialObject;
    using System;

    public class MoonNavigationPageModel : TabbedPageModel<CelestialObject>
    {
        [Obsolete("Only for page generation.")]
        public MoonNavigationPageModel()
            : base(NavigationServiceNameConstants.Moon)
        {
        }

        public MoonNavigationPageModel(CelestialObject model)
            : base(model, NavigationServiceNameConstants.Moon)
        {
        }

        protected override void OnModelPop()
        {
            AddTab<MoonImagePageModel>(Model.Code, string.Empty, Model);

            AddTab<MoonDetailPageModel>(Model.Code, string.Empty, Model);
        }
    }
}