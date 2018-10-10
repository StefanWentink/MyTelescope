namespace MyTelescope.App.Models.Base
{
    using DataLayer.Interfaces;
    using MyTelescope.Utilities.Interfaces;
    using ViewModels.Interfaces;

    public abstract class OverviewPageModel<TViewModel, TModel> : CollectionPageModel<TViewModel, TModel>
        where TViewModel : class, IBaseKeyViewModel<TModel>, new()
        where TModel : class, IKeyModel, new()
    {
        protected OverviewPageModel(IDataLoader<TViewModel, TModel> dataLoader)
            : base(dataLoader)
        {
        }
    }
}