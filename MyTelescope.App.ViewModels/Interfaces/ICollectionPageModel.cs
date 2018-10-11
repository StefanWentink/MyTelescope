namespace MyTelescope.App.ViewModels.Interfaces
{
    using MyTelescope.Utilities.Interfaces;
    using System.Collections.ObjectModel;

    public interface ICollectionPageModel<TViewModel, TModel> : IModelContainer<TModel>
        where TViewModel : class, IBaseViewModel
        where TModel : class, IKeyModel, new()
    {
        ObservableCollection<TViewModel> Collection { get; set; }
    }
}