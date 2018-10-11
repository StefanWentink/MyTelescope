namespace MyTelescope.App.Utilities.Models
{
    using System.Collections.Generic;

    public class CollectionsLoadContainer<TViewModel>
        where TViewModel : class
    {
        private Dictionary<string, CollectionLoadContainer<TViewModel>> _collections;

        private readonly object _collectionsLock = new object();

        public CollectionsLoadContainer()
        {
            lock (_collectionsLock)
            {
                _collections = new Dictionary<string, CollectionLoadContainer<TViewModel>>();
            }
        }

        protected Dictionary<string, CollectionLoadContainer<TViewModel>> LoadContainer
        {
            get
            {
                lock (_collectionsLock)
                {
                    return _collections;
                }
            }

            set
            {
                lock (_collectionsLock)
                {
                    _collections = value;
                }
            }
        }

        public CollectionLoadContainer<TViewModel> GetCollectionLoadContainer(string key)
        {
            lock (_collectionsLock)
            {
                if (!_collections.ContainsKey(key))
                {
                    _collections.Add(key, new CollectionLoadContainer<TViewModel>());
                }

                return _collections[key];
            }
        }

        ~CollectionsLoadContainer()
        {
            lock (_collectionsLock)
            {
                _collections = null;
            }
        }
    }
}