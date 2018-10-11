namespace MyTelescope.App.Models.Base
{
    using FreshMvvm;
    using MyTelescope.Utilities.Helpers;
    using SWE.Model.Interfaces;
    using System;
    using ViewModels.Interfaces;
    using Xamarin.Forms;

    public abstract class BasePageModel<TModel> :
        FreshBasePageModel,
        IModelContainer<TModel>
        where TModel : class, IKey, new()
    {
        public TModel Model { get; protected set; }

        [Obsolete("Only for page generation.")]
        protected BasePageModel()
        {
        }

        protected BasePageModel(TModel model)
        {
            SetModel(model);
        }

        public override void Init(object initData)
        {
            switch (initData)
            {
                case null:
                    return;

                case TModel model:
                    SetModel(model);
                    break;

                default:
                    throw new ArgumentException($"InitData {initData} is not of the expected type {initData.GetType()}.");
            }
        }

        public virtual void SetModel(TModel model)
        {
            if (!GuidHelper.GuidIsNullOrEmpty(model?.Id) && model != Model)
            {
                Model = model;
                ModelFetchedHandler();
                RaisePropertyChanged(nameof(Model));
            }
        }

        protected abstract void ModelFetchedHandler();

        public Command BackCommand
        {
            get
            {
                return new Command(async () => await CoreMethods.PopModalNavigationService().ConfigureAwait(false));
            }
        }

        protected override void ViewIsAppearing(object sender, EventArgs e)
        {
            SetModel(Model);
            base.ViewIsAppearing(sender, e);
        }

        ~BasePageModel()
        {
            Model = null;
        }
    }
}