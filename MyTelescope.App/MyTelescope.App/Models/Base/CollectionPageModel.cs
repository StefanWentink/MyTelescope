namespace MyTelescope.App.Models.Base
{
    using DataLayer.Enums;
    using DataLayer.Interfaces;
    using Helpers;
    using SWE.Model.Interfaces;
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;
    using Utilities.EventArgs;
    using ViewModels.Interfaces;
    using Xamarin.Forms;

    public abstract class CollectionPageModel<TViewModel, TModel> :
        BasePageModel<TModel>,
        ICollectionPageModel<TViewModel, TModel>
        where TViewModel : class, IBaseViewModel
        where TModel : class, IKey, new()
    {
        private object _collectionWriteLock = new object();

        private TViewModel _selectedItem;

        public int RecordCount { get; protected set; }

        public ObservableCollection<TViewModel> Collection { get; set; } = new ObservableCollection<TViewModel>();

        protected IDataLoader<TViewModel, TModel> DataLoader { get; }

        protected CollectionPageModel(TModel model)
            : base(model)
        {
        }

        protected CollectionPageModel(IDataLoader<TViewModel, TModel> dataLoader)
            : base(null)
        {
            if (DataLoader == null && dataLoader != null)
            {
                DataLoader = dataLoader;
                DataLoader.EndOfCollectionEvent += EndOfCollectionHandler;
                DataLoader.CollectionFetchedEvent += CollectionFetchedHandler;
            }
        }

        public TViewModel SelectedItem
        {
            get => null;
            set
            {
                if (_selectedItem != null)
                {
                    _selectedItem.Selected = false;
                }

                _selectedItem = value;

                if (_selectedItem != null)
                {
                    _selectedItem.Selected = true;
                    Task.Run(() => ItemSelected(_selectedItem).ConfigureAwait(false)).ConfigureAwait(false);
                }
            }
        }

        protected virtual bool AddToCollection { get; set; } = true;

        protected abstract Task ItemSelected(TViewModel selectedItem);

        protected override void ModelFetchedHandler()
        {
            Collection.Clear();
            DataLoader.Load(DataLoading.Refresh, Model);
        }

        protected virtual void CollectionFetchedHandler(object sender, CollectionFetchedEventArgs<TViewModel> args)
        {
            if (args.Models == null || args.Models.Count == 0)
            {
                return;
            }

            Collection.PutOnApplicationThread(args.Models, _collectionWriteLock, RaisePropertyChanged, nameof(Collection), !AddToCollection, CollectionSet);
        }

        protected virtual void EndOfCollectionHandler(object sender, EndOfCollectionEventArgs args)
        {
            RecordCount = args.Count;
        }

        protected virtual void CollectionSet()
        {
        }

        public Command RefreshDataCommand
        {
            get
            {
                return new Command(async () =>
                {
                    Collection.Clear();
                    await DataLoader.LoadAsync(DataLoading.Reset, Model).ConfigureAwait(false);
                });
            }
        }

        protected override void ViewIsDisappearing(object sender, EventArgs e)
        {
            if (DataLoader != null)
            {
                DataLoader.EndOfCollectionEvent -= EndOfCollectionHandler;
                DataLoader.CollectionFetchedEvent -= CollectionFetchedHandler;
            }

            base.ViewIsDisappearing(sender, e);
        }

        protected override void ViewIsAppearing(object sender, EventArgs e)
        {
            if (DataLoader != null)
            {
                DataLoader.EndOfCollectionEvent += EndOfCollectionHandler;
                DataLoader.CollectionFetchedEvent += CollectionFetchedHandler;
            }

            base.ViewIsAppearing(sender, e);
        }

        ~CollectionPageModel()
        {
            if (DataLoader != null)
            {
                DataLoader.EndOfCollectionEvent -= EndOfCollectionHandler;
                DataLoader.CollectionFetchedEvent -= CollectionFetchedHandler;
            }

            _collectionWriteLock = null;

            Collection = null;
        }
    }
}