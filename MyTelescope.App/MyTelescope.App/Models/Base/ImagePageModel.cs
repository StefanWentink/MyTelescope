namespace MyTelescope.App.Models.Base
{
    using SWE.Model.Interfaces;
    using System;
    using ViewModels.Interfaces;

    public abstract class ImagePageModel<TViewModel, TModel> : DetailPageModel<TViewModel, TModel>
        where TViewModel : class, IBaseKeyViewModel<TModel>, new()
        where TModel : class, IKey, new()
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