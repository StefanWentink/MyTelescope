﻿namespace MyTelescope.App.Pages.Content
{
    using System;
    using Models.Base;
    using SolarSystem.Models.CelestialObject;
    using ViewModels.Models.Item;

    public class PlanetImagePageModel : ImagePageModel<CelestialObjectViewModel, CelestialObjectModel>
    {
        [Obsolete("Only for page generation.")]
        public PlanetImagePageModel()
            : base(null)
        {
        }

        public PlanetImagePageModel(CelestialObjectModel model)
            : base(model)
        {
        }
    }
}