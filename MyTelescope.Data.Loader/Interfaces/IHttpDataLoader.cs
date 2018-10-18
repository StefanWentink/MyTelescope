namespace MyTelescope.Data.Loader.Interfaces
{
    using MyTelescope.App.ViewModels.Interfaces;
    using SWE.Model.Interfaces;

    public interface IHttpDataLoader<TViewModel, TModel> : IDataLoader<TViewModel, TModel>
        where TViewModel : class, IBaseKeyViewModel<TModel>, new()
        where TModel : class, IKey, new()
    {
    }
}