namespace MyTelescope.Data.Loader.Interfaces
{
    using MyTelescope.App.Utilities.EventArgs;
    using MyTelescope.App.ViewModels.Interfaces;
    using SWE.Http.Enums;
    using SWE.Http.Interfaces;
    using SWE.Model.Interfaces;
    using System;
    using System.Threading.Tasks;

    public interface IDataLoader<TViewModel, in TModel>
        where TViewModel : class, IBaseViewModel
        where TModel : class, IKey, new()
    {
        ISecurityToken SecurityToken { set; }

        void Load(DataLoading dataLoading, TModel model);

        Task LoadAsync(DataLoading dataLoading, TModel model);

        event EventHandler<CollectionFetchedEventArgs<TViewModel>> CollectionFetchedEvent;

        //event EventHandler<EndOfCollectionEventArgs> EndOfCollectionEvent;
    }
}