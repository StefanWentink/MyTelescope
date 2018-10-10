namespace MyTelescope.App.ViewModels.Models
{
    using System;
    using Interfaces;
    using MyTelescope.Utilities.Interfaces;

    public abstract class BaseKeyViewModel<TModel> : BaseViewModel, IBaseKeyViewModel<TModel>, IKeyModel
        where TModel : class, IKeyModel, new()
    {
        public Guid Id
        {
            get => Model.Id;
            set => Model.Id = value;
        }

        public TModel Model { get; set; }

        protected BaseKeyViewModel()
            : this(new TModel())
        {
        }

        protected BaseKeyViewModel(TModel model)
        {
            Model = model;
        }

        ~BaseKeyViewModel()
        {
            Model = null;
        }
    }
}
