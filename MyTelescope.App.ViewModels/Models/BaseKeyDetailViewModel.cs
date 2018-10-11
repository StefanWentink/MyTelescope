namespace MyTelescope.App.ViewModels.Models
{
    using Interfaces;
    using SWE.Model.Interfaces;
    using System;
    using System.Collections.Generic;
    using Utilities.EventArgs;

    public abstract class BaseKeyDetailViewModel<TModel> : BaseKeyViewModel<TModel>, IBaseKeyDetailViewModel<TModel>
        where TModel : class, IKey, new()
    {
        public event EventHandler<CollectionFetchedEventArgs<IDetailViewModel>> CollectionFetched;

        protected BaseKeyDetailViewModel()
            : this(new TModel())
        {
        }

        protected BaseKeyDetailViewModel(TModel model)
            : base(model)
        {
        }

        public void LoadDetails()
        {
            CollectionFetched?.Invoke(this, new CollectionFetchedEventArgs<IDetailViewModel>(GetDetails()));
        }

        public abstract List<IDetailViewModel> GetDetails();

        ~BaseKeyDetailViewModel()
        {
            Model = null;
        }
    }
}