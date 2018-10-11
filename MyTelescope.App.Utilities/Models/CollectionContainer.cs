namespace MyTelescope.App.Utilities.Models
{
    using System.Collections.Generic;

    public class CollectionContainer<TViewModel>
    {
        private List<TViewModel> _collection;

        private readonly object _collectionLock = new object();

        public CollectionContainer()
        {
            ClearCollection();
        }

        public void ClearCollection()
        {
            SetCollection(new List<TViewModel>());
        }

        public void SetCollection(List<TViewModel> collection)
        {
            lock (_collectionLock)
            {
                _collection = collection;
            }
        }

        public void AddCollection(List<TViewModel> collection)
        {
            lock (_collectionLock)
            {
                _collection.AddRange(collection);
            }
        }

        public List<TViewModel> GetCollection()
        {
            lock (_collectionLock)
            {
                return _collection;
            }
        }

        public int CollectionCount()
        {
            lock (_collectionLock)
            {
                return _collection.Count;
            }
        }

        ~CollectionContainer()
        {
            lock (_collectionLock)
            {
                _collection = null;
            }
        }
    }
}