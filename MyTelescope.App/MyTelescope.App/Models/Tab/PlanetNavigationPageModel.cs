namespace MyTelescope.App.Pages.Tab
{
    using Constants;
    using Content;
    using Localisation.Resources.MyTelescope;
    using Models.Base;
    using SolarSystem.Models.CelestialObject;
    using System;

    public class PlanetNavigationPageModel : TabbedPageModel<CelestialObjectModel>
    {
        [Obsolete("Only for page generation.")]
        public PlanetNavigationPageModel()
            : base(NavigationServiceNameConstants.Planet)
        {
        }

        public PlanetNavigationPageModel(CelestialObjectModel model)
            : base(model, NavigationServiceNameConstants.Planet)
        {
        }

        protected override void OnModelPop()
        {
            AddTab<PlanetImagePageModel>(Model.Code, string.Empty, Model);

            AddTab<PlanetDetailPageModel>(TextResource.Details, string.Empty, Model);

            if (Model.Satellites > 0)
            {
                AddTab<PlanetMoonOverviewPageModel>(TextResource.MajorMoons, string.Empty, Model);

                AddTab<MoonComparePageModel>(TextResource.Compare, string.Empty, Model);
            }

            //if (Model.InferiorOrbit)
            //{
            //    // Tab showing inferior orbit  greatest elongation
            //    AddTab<PlanetMoonOverviewPageModel>(TextResource.GreatestElongation, string.Empty, model);
            //}
            //else
            //{
            //    AddTab<PlanetMoonOverviewPageModel>(TextResource.Opposition, string.Empty, model);
            //}
        }
    }
}