namespace MyTelescope.App.Pages.Content
{
    using Models.Base;
    using SolarSystem.Models.CelestialObject;
    using System;
    using ViewModels.Models.Item;

    public class MoonImagePageModel : ImagePageModel<CelestialObjectViewModel, CelestialObject>
    {
        [Obsolete("Only for page generation.")]
        public MoonImagePageModel()
            : base(null)
        {
        }

        public MoonImagePageModel(CelestialObject model)
            : base(model)
        {
        }
    }
}