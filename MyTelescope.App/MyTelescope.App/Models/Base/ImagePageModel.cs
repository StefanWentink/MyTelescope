﻿namespace MyTelescope.App.Models.Base
{
    using System;
    using MyTelescope.Utilities.Interfaces;
    using ViewModels.Interfaces;

    public abstract class ImagePageModel<TViewModel, TModel> : DetailPageModel<TViewModel, TModel>
        where TViewModel : class, IBaseKeyViewModel<TModel>, new()
        where TModel : class, IKeyModel, new()
    {
        [Obsolete("Only for page generation.")]
        protected ImagePageModel()
        {
        }

        protected ImagePageModel(TModel model)
            : base(model)
        {
        }
    }
}