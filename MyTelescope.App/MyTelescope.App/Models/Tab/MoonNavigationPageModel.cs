namespace MyTelescope.App.Pages.Tab
{
    using System;
    using Constants;
    using Content;
    using Models.Base;
    using SolarSystem.Models.CelestialObject;

    public class MoonNavigationPageModel : TabbedPageModel<CelestialObjectModel>
    {
        [Obsolete("Only for page generation.")]
        public MoonNavigationPageModel()
            : base(NavigationServiceNameConstants.Moon)
        {
        }

        public MoonNavigationPageModel(CelestialObjectModel model)
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
