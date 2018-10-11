namespace MyTelescope.App.Models.Base
{
    using FreshMvvm;
    using MyTelescope.Utilities.Helpers;
    using SWE.Model.Interfaces;
    using System;
    using ViewModels.Interfaces;

    public abstract class TabbedPageModel<TModel> :
        FreshTabbedNavigationContainer,
        IModelContainer<TModel>
        where TModel : class, IKey, new()
    {
        public TModel Model { get; set; }

        public virtual void SetModel(TModel model)
        {
            if (Model?.Id != model?.Id)
            {
                Model = null;

                if (model != null)
                {
                    Model = model;
                    OnModelPop();
                }
            }
        }

        protected abstract void OnModelPop();

        [Obsolete("Only for page generation.")]
        protected TabbedPageModel(string navigationServiceName)
            : base(navigationServiceName)
        {
        }

        protected TabbedPageModel(TModel model, string navigationServiceName)
            : base(navigationServiceName)
        {
            Setup(model);
        }

        private void Setup(TModel model)
        {
            if (!GuidHelper.GuidIsNullOrEmpty(model?.Id ?? Guid.Empty))
            {
                SetModel(model);
            }
        }
    }
}