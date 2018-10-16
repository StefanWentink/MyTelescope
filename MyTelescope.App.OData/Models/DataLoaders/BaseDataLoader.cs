namespace MyTelescope.App.OData.Models.DataLoader
{
    using Enums;
    using Interfaces;
    using MyTelescope.Utilities.Helpers;
    using SWE.Model.Interfaces;
    using MyTelescope.Utilities.Models.Filter;
    using MyTelescope.Utilities.Models.Sort;
    using SolarSystem.Models.CelestialObject;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Utilities.EventArgs;
    using Utilities.Helpers;
    using Utilities.Models;
    using ViewModels.Interfaces;
    using SWE.OData.Interfaces;
    using SWE.OData.Builders;
    using SWE.OData.Models;
    using SWE.OData.Enums;

    public abstract class BaseDataLoader<TView, T> : IDataLoader<TView, T>
        where TView : class, IBaseViewModel
        where T : class, IKey, new()
    {
        private readonly object _filterLock = new object();

        private CollectionsLoadContainer<TView> _collectionsLoadContainer;

        protected virtual string EntityName { get; set; }

        public CollectionLoadContainer<TView> GetCollectionsLoadContainer(string key)
        {
            lock (_collectionsLoadContainer)
            {
                return _collectionsLoadContainer.GetCollectionLoadContainer(key);
            }
        }

        protected BaseDataLoader()
        {
            _collectionsLoadContainer = new CollectionsLoadContainer<TView>();
        }

        protected virtual IODataFilters<T> DefaultFilters => null;

        /// <summary>
        /// Provide filters based on <see cref="T"/>
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        protected virtual IODataFilters<T> GetModelFilters(T model)
        {
            return null;
        }

        private IODataFilters<T> GetFilters(T model)
        {
            var defaultFilters = DefaultFilters;
            var modelFilters = GetModelFilters(model);

            if (defaultFilters != null)
            {
                if (modelFilters != null)
                {
                    return new ODataFilters<T>(QueryOperator.And, defaultFilters, modelFilters);
                }

                return defaultFilters;
            }

            return modelFilters;
        }

        internal IODataBuilder<T, Guid> GetFilter(T model)
        {
            var filters = GetFilters(model);
            var sort = GetSort();
            IODataBuilder<T, Guid> result = new ODataBuilder<T, Guid>(EntityName);

            if (filters != null)
            {
                result.SetFilter(filters);
            }

            if (sort.SortItems.Any())
            {
                var sortItem = sort.SortItems.First();
                result = result.SetOrder(sortItem.Column)
                    .SetDescending(!sortItem.Ascending);
            }
            return result;
        }

        protected virtual SortModel GetSort()
        {
            return new SortModel(nameof(CelestialObjectPosition.CelestialObjectId), true);
        }

        public async Task LoadAsync(DataLoading dataLoading, T model)
        {
            var filter = GetFilter(model);
            var filterKey = filter.BuilderKey();

            switch (dataLoading)
            {
                case DataLoading.Load:
                case DataLoading.BatchLoad:
                    await LoadModels(model, filter, dataLoading).ConfigureAwait(false);
                    break;

                case DataLoading.Preload:
                    await LoadModels(model, filter, dataLoading).ConfigureAwait(false);
                    break;

                case DataLoading.Refresh:
                    var collection = GetCollectionsLoadContainer(filterKey).GetCollection();
                    OnCollectionFetched(collection);

                    if (GetCollectionsLoadContainer(filterKey).GetEndOfCollectionWithNoRunningTasks())
                    {
                        OnEndOfCollection(filterKey);
                    }
                    else if (!GetCollectionsLoadContainer(filterKey).GetEndOfCollection())
                    {
                        await LoadModels(model, filter, dataLoading).ConfigureAwait(false);
                    }

                    break;

                case DataLoading.Reset:
                    Reset();
                    await LoadModels(model, filter, dataLoading).ConfigureAwait(false);
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(dataLoading), dataLoading, null);
            }
        }

        public void Load(DataLoading dataLoading, T model)
        {
            Task.Run(() => LoadAsync(dataLoading, model).ConfigureAwait(false));
        }

        public void Reset()
        {
            lock (_collectionsLoadContainer)
            {
                _collectionsLoadContainer = new CollectionsLoadContainer<TView>();
            }
        }

        public event EventHandler<CollectionFetchedEventArgs<TView>> CollectionFetchedEvent;

        public event EventHandler<EndOfCollectionEventArgs> EndOfCollectionEvent;

        protected void PopToList(string filterKey, List<TView> collection, DataLoading dataLoading, bool singleRequest)
        {
            if (collection.Count > 0)
            {
                GetCollectionsLoadContainer(filterKey).AddCollection(collection);

                if (!dataLoading.In(DataLoading.Preload))
                {
                    OnCollectionFetched(collection);
                }
            }

            if (singleRequest || collection.Count == 0)
            {
                GetCollectionsLoadContainer(filterKey).SetEndOfCollection();
            }

            if (!dataLoading.In(DataLoading.Preload) && GetCollectionsLoadContainer(filterKey).GetEndOfCollectionWithNoRunningTasksAndEndOfCollectionNotThrown())
            {
                OnEndOfCollection(filterKey);
            }
        }

        protected async Task LoadModels(T model, IODataBuilder<T, Guid> filter, DataLoading dataLoading)
        {
            var filterKey = filter.BuilderKey();
            var tasks = new List<Task<List<TView>>>();
            var recordRequestNumber = GetCollectionsLoadContainer(filterKey).GetRecordRequestNumber();

            if (recordRequestNumber == int.MaxValue
                || GetCollectionsLoadContainer(filterKey).GetEndOfCollection()
                || GetCollectionsLoadContainer(filterKey).RunningTaskCount() > 0)
            {
                return;
            }

            var singleRequest = false;

            foreach (var batch in GetSortModels(dataLoading, recordRequestNumber))
            {
                singleRequest = batch.RecordRequestNumber == int.MaxValue;

                if (batch.RecordRequestNumber > GetCollectionsLoadContainer(filterKey).GetRecordRequestNumber())
                {
                    GetCollectionsLoadContainer(filterKey).AddRecordRequestNumber(batch.Take);
                    lock (_filterLock)
                    {
                        filter.SetSkip(batch.Skip).SetTop(batch.Take);
                        tasks.Add(GetTask(model, filter));
                        GetCollectionsLoadContainer(filterKey).AddRunningTask();
                    }
                }
            }

            while (tasks.Count > 0)
            {
                var completedTask = await Task.WhenAny(tasks).ConfigureAwait(false);
                GetCollectionsLoadContainer(filterKey).RemoveRunningTask();
                PopToList(filterKey, completedTask.Result, dataLoading, singleRequest);
                tasks.Remove(completedTask);
            }
        }

        protected abstract Task<List<TView>> GetTask(T model, IODataBuilder<T, Guid> filter);

        protected virtual IEnumerable<SortModel> GetSortModels(DataLoading dataLoading, int recordRequestNumber)
        {
            if (dataLoading == DataLoading.BatchLoad)
            {
                foreach (var batch in BatchHelper.GetSortModels(recordRequestNumber))
                {
                    yield return batch;
                }
            }
            else
            {
                yield return new SortModel(recordRequestNumber, int.MaxValue);
            }
        }

        protected virtual void OnCollectionFetched(List<TView> models)
        {
            if (models.Count > 0)
            {
                CollectionFetchedEvent?.Invoke(this, new CollectionFetchedEventArgs<TView>(models));
            }
        }

        protected virtual void OnEndOfCollection(string filterKey)
        {
            EndOfCollectionEvent?.Invoke(this, new EndOfCollectionEventArgs(GetCollectionsLoadContainer(filterKey).CollectionCount()));
        }

        ~BaseDataLoader()
        {
            lock (_collectionsLoadContainer)
            {
                _collectionsLoadContainer = null;
            }

            if (CollectionFetchedEvent == null)
            {
                CollectionFetchedEvent = null;
            }

            if (EndOfCollectionEvent == null)
            {
                EndOfCollectionEvent = null;
            }
        }
    }
}