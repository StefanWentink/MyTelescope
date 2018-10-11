﻿namespace MyTelescope.App.ViewModels.Models.Item
{
    using Interfaces;
    using System;

    public class PlanetDetailViewModel : DetailViewModel
    {
        [Obsolete("Only for model generation.")]
        public PlanetDetailViewModel()
            : base(string.Empty, string.Empty)
        {
        }

        public PlanetDetailViewModel(
            IDetailViewModel detailView)
            : base(detailView.Code, detailView.Description)
        {
        }
    }
}