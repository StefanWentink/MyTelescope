namespace MyTelescope.App.DataLayer.Interfaces
{
    using MyTelescope.Utilities.Interfaces;
    using ViewModels.Interfaces;

    public interface IStaticDataLoader<TViewModel, in TModel> : IDataLoader<TViewModel, TModel>
        where TViewModel : class, IBaseViewModel
        where TModel : class, IKeyModel, new()
    {
    }
}