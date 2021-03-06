﻿namespace MyTelescope.App.Pages.Content
{
    using DataLayer.Interfaces;
    using Models.Base;
    using MyTelescope.Utilities.Helpers;
    using SolarSystem.Models.CelestialObject;
    using ViewModels.Models.Item;

    public class MoonComparePageModel : CelestialComparePageModel
    {
        public override string CanvasViewKey => ModelHelper.GetName(GetType().Name);

        public MoonComparePageModel(IHttpDataLoader<CelestialObjectViewModel, CelestialObjectModel> dataLoader)
            : base(dataLoader)
        {
        }
    }
}