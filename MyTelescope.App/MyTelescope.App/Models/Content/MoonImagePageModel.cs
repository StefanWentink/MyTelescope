﻿namespace MyTelescope.App.Pages.Content
{
    using System;
    using Models.Base;
    using SolarSystem.Models.CelestialObject;
    using ViewModels.Models.Item;

    public class MoonImagePageModel : ImagePageModel<CelestialObjectViewModel, CelestialObjectModel>
    {
        [Obsolete("Only for page generation.")]
        public MoonImagePageModel()
            : base(null)
        {
        }

        public MoonImagePageModel(CelestialObjectModel model)
            : base(model)
        {
        }
    }
}