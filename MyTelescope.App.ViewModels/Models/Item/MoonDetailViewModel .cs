﻿namespace MyTelescope.App.ViewModels.Models.Item
{
    using System;
    using Interfaces;

    public class MoonDetailViewModel : DetailViewModel
    {
        [Obsolete("Only for model generation.")]
        public MoonDetailViewModel()
            : base(string.Empty, string.Empty)
        {
        }

        public MoonDetailViewModel(
            IDetailViewModel detailView)
            : base(detailView.Code, detailView.Description)
        {
        }
    }
}
