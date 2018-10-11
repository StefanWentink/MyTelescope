namespace MyTelescope.App.ViewModels.Interfaces
{
    using SWE.Model.Interfaces;
    using System.Collections.ObjectModel;

    public interface ICollectionPageModel<TViewModel, TModel> : IModelContainer<TModel>
        where TViewModel : class, IBaseViewModel
        where TModel : class, IKey, new()
    {
        ObservableCollection<TViewModel> Collection { get; set; }
    }
}