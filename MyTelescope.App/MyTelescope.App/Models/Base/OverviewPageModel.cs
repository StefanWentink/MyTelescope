namespace MyTelescope.App.Models.Base
{
    using MyTelescope.Data.Loader.Interfaces;
    using SWE.Model.Interfaces;
    using ViewModels.Interfaces;

    public abstract class OverviewPageModel<TViewModel, TModel> : CollectionPageModel<TViewModel, TModel>
        where TViewModel : class, IBaseKeyViewModel<TModel>, new()
        where TModel : class, IKey, new()
    {
        protected OverviewPageModel(IDataLoader<TViewModel, TModel> dataLoader)
            : base(dataLoader)
        {
        }
    }
}