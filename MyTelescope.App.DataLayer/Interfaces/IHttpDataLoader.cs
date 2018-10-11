namespace MyTelescope.App.DataLayer.Interfaces
{
    using MyTelescope.Utilities.Interfaces;
    using ViewModels.Interfaces;

    public interface IHttpDataLoader<TViewModel, TModel> : IDataLoader<TViewModel, TModel>
        where TViewModel : class, IBaseKeyViewModel<TModel>, new()
        where TModel : class, IKeyModel, new()
    {
    }
}