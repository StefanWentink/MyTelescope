namespace MyTelescope.App.ViewModels.Models
{
    using Interfaces;
    using SWE.Model.Interfaces;

    using System;

    public abstract class BaseKeyViewModel<TModel> : BaseViewModel, IBaseKeyViewModel<TModel>, IKey
        where TModel : class, IKey, new()
    {
        public Guid Id
        {
            get => Model.Id;
            //set => Model.Id = value;
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