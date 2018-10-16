namespace MyTelescope.App.OData.Interfaces
{
    using SWE.Model.Interfaces;
    using ViewModels.Interfaces;

    public interface IStaticDataLoader<TViewModel, in TModel> : IDataLoader<TViewModel, TModel>
        where TViewModel : class, IBaseViewModel
        where TModel : class, IKey, new()
    {
    }
}