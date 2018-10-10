namespace MyTelescope.App.DataLayer.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Enums;
    using MyTelescope.Utilities.Interfaces;
    using MyTelescope.Utilities.Models.Filter;
    using Utilities.EventArgs;
    using ViewModels.Interfaces;

    public interface IDataLoader<TViewModel, in TModel>
        where TViewModel : class, IBaseViewModel
        where TModel : class, IKeyModel, new()
    {
        void Load(DataLoading dataLoading, TModel model);

        Task LoadAsync(DataLoading dataLoading, TModel model);

        event EventHandler<CollectionFetchedEventArgs<TViewModel>> CollectionFetchedEvent;

        event EventHandler<EndOfCollectionEventArgs> EndOfCollectionEvent;
    }
}
