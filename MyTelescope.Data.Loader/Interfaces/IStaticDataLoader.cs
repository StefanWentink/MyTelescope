namespace MyTelescope.Data.Loader.Interfaces
{
    using MyTelescope.App.ViewModels.Interfaces;
    using SWE.Model.Interfaces;

    public interface IStaticDataLoader<TViewModel, in TModel> : IDataLoader<TViewModel, TModel>
        where TViewModel : class, IBaseViewModel
        where TModel : class, IKey, new()
    {
    }
}