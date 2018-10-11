namespace MyTelescope.App.Models.Base
{
    using MyTelescope.Utilities.Interfaces;
    using System;
    using ViewModels.Interfaces;

    public abstract class DetailPageModel<TViewModel, TModel> : BasePageModel<TModel>
        where TViewModel : class, IBaseKeyViewModel<TModel>, new()
        where TModel : class, IKeyModel, new()
    {
        public TViewModel ViewModel { get; set; }

        [Obsolete("Only for page generation.")]
        protected DetailPageModel()
        {
        }

        protected DetailPageModel(TModel model)
            : base(model)
        {
        }

        protected override void ModelFetchedHandler()
        {
            ViewModel = new TViewModel { Model = Model };
            RaisePropertyChanged(nameof(ViewModel));
        }
    }
}