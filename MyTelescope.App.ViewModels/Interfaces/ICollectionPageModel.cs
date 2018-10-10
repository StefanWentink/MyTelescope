namespace MyTelescope.App.ViewModels.Interfaces
{
    using System.Collections.ObjectModel;
    using MyTelescope.Utilities.Interfaces;

    public interface ICollectionPageModel<TViewModel, TModel> : IModelContainer<TModel>
        where TViewModel : class, IBaseViewModel
        where TModel : class, IKeyModel, new()
    {
        ObservableCollection<TViewModel> Collection { get; set; }
    }
}
