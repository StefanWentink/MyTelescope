namespace MyTelescope.App.OData.Models.DataLoader
{
    using SWE.Http.Enums;
    using MyTelescope.Utilities.Helpers;
    using SWE.Model.Interfaces;
    using MyTelescope.Utilities.Models.Sort;
    using SolarSystem.Models.CelestialObject;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Utilities.EventArgs;
    using Utilities.Helpers;
    using Utilities.Models;
    using ViewModels.Interfaces;
    using SWE.OData.Interfaces;
    using SWE.OData.Builders;
    using SWE.OData.Models;
    using System.Threading;
    using SWE.Http.Interfaces;
    using MyTelescope.Data.Loader.Interfaces;
    using MyTelescope.App.Utilities.Interfaces;

    public abstract class BaseDataLoader<TView, T> : IDataLoader<TView, T>
        where TView : class, IBaseViewModel
        where T : class, IKey, new()
    {
        private readonly object _filterLock = new object();

        private readonly IBatchContainer _batchContainer;

        private CollectionsLoadContainer<TView> _collectionsLoadContainer;

        protected virtual string EntityName { get; set; }

        public virtual ISecurityToken SecurityToken { protected get; set; }

        public CollectionLoadContainer<TView> GetCollectionsLoadContainer(string key)
        {
            lock (_collectionsLoadContainer)
            {
                return _collectionsLoadContainer.GetCollectionLoadContainer(key);
            }
        }

        protected BaseDataLoader(IBatchContainer batchContainer)
        {
            _collectionsLoadContainer = new CollectionsLoadContainer<TView>();
            _batchContainer = batchContainer;
        }

        protected virtual List<IODataFilter> DefaultFilters => null;

        /// <summary>
        /// Provide filters based on <see cref="T"/>
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        protected virtual List<IODataFilter> GetModelFilters(T model)
        {
            return null;
        }

        private IODataFilters GetFilters(T model)
        {
            var defaultFilters = DefaultFilters;
            var modelFilters = GetModelFilters(model);

            if (defaultFilters?.Count > 0)
            {
                var result = new ODataFilters(defaultFilters);

                if (modelFilters?.Count > 0)
                {
                    result.AddFilter(modelFilters);
                }

                return result;
            }

            if (modelFilters?.Count > 0)
            {
                return new ODataFilters(modelFilters);
            }

            return null;
        }

        internal IODataBuilder<T, Guid> GetFilter(T model)
        {
            var filters = GetFilters(model);
            var sort = GetSort();
            IODataBuilder<T, Guid> result = new ODataBuilder<T, Guid>(EntityName ?? typeof(T).Name);

            if (filters != null)
            {
                result.SetFilter(filters);
            }

            if (sort.SortItems.Count > 0)
            {
                var sortItem = sort.SortItems[0];
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
            // TODO - fix
            //LoadAsync(dataLoading, model);
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

        protected void PopToList(string filterKey, List<TView> collection, DataLoading dataLoading, bool finalRequest)
        {
            if (collection.Count > 0)
            {
                GetCollectionsLoadContainer(filterKey).AddCollection(collection);

                if (!dataLoading.In(DataLoading.Preload))
                {
                    OnCollectionFetched(collection);
                }
            }

            if (finalRequest)
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
            var tasks = new List<Task<(int requestedCount, List<TView> items)>>();
            var recordRequestNumber = GetCollectionsLoadContainer(filterKey).GetRecordRequestNumber();

            if (recordRequestNumber == int.MaxValue
                || GetCollectionsLoadContainer(filterKey).GetEndOfCollection()
                || GetCollectionsLoadContainer(filterKey).RunningTaskCount() > 0)
            {
                return;
            }

            var finalRequest = false;

            var cancelationToken = new CancellationToken();

            foreach (var batch in GetSortModels(dataLoading, recordRequestNumber))
            {
                finalRequest = batch.RecordRequestNumber == int.MaxValue;

                if (batch.RecordRequestNumber > GetCollectionsLoadContainer(filterKey).GetRecordRequestNumber())
                {
                    GetCollectionsLoadContainer(filterKey).AddRecordRequestNumber(batch.Take);
                    lock (_filterLock)
                    {
                        filter.SetSkip(batch.Skip).SetTop(batch.Take);
                        tasks.Add(GetTask(model, batch.Take, cancelationToken, SecurityToken, filter));
                        GetCollectionsLoadContainer(filterKey).AddRunningTask();
                    }
                }
            }

            while (tasks.Count > 0)
            {
                var completedTask = await Task.WhenAny(tasks).ConfigureAwait(false);

                tasks.Remove(completedTask);

                GetCollectionsLoadContainer(filterKey).RemoveRunningTask();

                var result = completedTask.Result.items;

                finalRequest = finalRequest || result.Count < completedTask.Result.requestedCount;

                PopToList(filterKey, result, dataLoading, finalRequest);
            }

            if (!finalRequest && dataLoading.In(DataLoading.Load, DataLoading.Preload))
            {
                await LoadModels(model, filter, dataLoading).ConfigureAwait(false);
            }
        }

        protected abstract Task<(int requestedCount, List<TView> items)> GetTask(
            T model,
            int requestedCount,
            CancellationToken cancellationToken,
            ISecurityToken securityToken,
            IODataBuilder<T, Guid> filter);

        protected virtual IEnumerable<SortModel> GetSortModels(DataLoading dataLoading, int recordRequestNumber)
        {
            foreach (var batch in _batchContainer.GetSortModels(recordRequestNumber, dataLoading == DataLoading.BatchLoad))
            {
                yield return batch;
            }
        }

        protected virtual void OnCollectionFetched(List<TView> models)
        {
            if (models.Count > 0)
            {
                CollectionFetchedEvent?.Invoke(this, new CollectionFetchedEventArgs<TView>(models));
            }
        }

        //protected virtual void OnEndOfCollection(string filterKey)
        //{
        //    EndOfCollectionEvent?.Invoke(this, new EndOfCollectionEventArgs(GetCollectionsLoadContainer(filterKey).CollectionCount()));
        //}

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