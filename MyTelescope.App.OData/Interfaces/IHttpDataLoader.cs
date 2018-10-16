namespace MyTelescope.App.OData.Interfaces
{
    using SWE.Model.Interfaces;
    using ViewModels.Interfaces;

    public interface IHttpDataLoader<TViewModel, TModel> : IDataLoader<TViewModel, TModel>
        where TViewModel : class, IBaseKeyViewModel<TModel>, new()
        where TModel : class, IKey, new()
    {
    }
}