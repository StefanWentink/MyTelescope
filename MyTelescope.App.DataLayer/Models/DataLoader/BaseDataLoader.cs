namespace MyTelescope.App.DataLayer.Models.DataLoader
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Enums;
    using Interfaces;
    using MyTelescope.Utilities.Helpers;
    using MyTelescope.Utilities.Interfaces;
    using MyTelescope.Utilities.Models.Filter;
    using MyTelescope.Utilities.Models.Sort;
    using SolarSystem.Models.CelestialObject;
    using Utilities.EventArgs;
    using Utilities.Helpers;
    using Utilities.Models;
    using ViewModels.Interfaces;

    public abstract class BaseDataLoader<TViewModel, TModel> : IDataLoader<TViewModel, TModel>
        where TViewModel : class, IBaseViewModel
        where TModel : class, IKeyModel, new()
    {
        private readonly object _filterLock = new object();

        private CollectionsLoadContainer<TViewModel> _collectionsLoadContainer;

        public CollectionLoadContainer<TViewModel> GetCollectionsLoadContainer(string key)
        {
            lock (_collectionsLoadContainer)
            {
                return _collectionsLoadContainer.GetCollectionLoadContainer(key);
            }
        }

        protected BaseDataLoader()
        {
            _collectionsLoadContainer = new CollectionsLoadContainer<TViewModel>();
        }
        
        protected virtual List<FilterItemModel> DefaultFilterItems => new List<FilterItemModel>();

        /// <summary>
        /// Provide filters based on <see cref="TModel"/>
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        protected virtual List<FilterItemModel> GetModelFilterItems(TModel model)
        {
            return new List<FilterItemModel>();
        }

        private List<FilterItemModel> GetFilterItems(TModel model)
        {
            var result = DefaultFilterItems;

            result.AddRange(GetModelFilterItems(model));

            return result;
        }

        internal FilterModel GetFilter(TModel model)
        {
            var filterItems = GetFilterItems(model);
            var sort = GetSort();

            return new FilterModel(sort, filterItems);
        }

        protected virtual SortModel GetSort()
        {
            return new SortModel($"{nameof(CelestialObjectPositionModel.CelestialObjectId)}", true);
        }

        public async Task LoadAsync(DataLoading dataLoading, TModel model)
        {
            var filter = GetFilter(model);
            var filterKey = filter.Key;

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

        public void Load(DataLoading dataLoading, TModel model)
        {
            Task.Run(() => LoadAsync(dataLoading, model).ConfigureAwait(false));
        }

        public void Reset()
        {
            lock (_collectionsLoadContainer)
            {
                _collectionsLoadContainer = new CollectionsLoadContainer<TViewModel>();
            }
        }

        public event EventHandler<CollectionFetchedEventArgs<TViewModel>> CollectionFetchedEvent;

        public event EventHandler<EndOfCollectionEventArgs> EndOfCollectionEvent;

        protected void PopToList(string filterKey, List<TViewModel> collection, DataLoading dataLoading, bool singleRequest)
        {
            if (collection.Any())
            {
                GetCollectionsLoadContainer(filterKey).AddCollection(collection);

                if (!dataLoading.In(DataLoading.Preload))
                {
                    OnCollectionFetched(collection);
                }
            }

            if(singleRequest || !collection.Any())
            {
                GetCollectionsLoadContainer(filterKey).SetEndOfCollection();
            }

            if (!dataLoading.In(DataLoading.Preload) && GetCollectionsLoadContainer(filterKey).GetEndOfCollectionWithNoRunningTasksAndEndOfCollectionNotThrown())
            {
                OnEndOfCollection(filterKey);
            }
        }

        protected async Task LoadModels(TModel model, FilterModel filter, DataLoading dataLoading)
        {
            var filterKey = filter.Key;
            var tasks = new List<Task<List<TViewModel>>>();
            var recordRequestNumber = GetCollectionsLoadContainer(filterKey).GetRecordRequestNumber();

            if (recordRequestNumber == int.MaxValue || 
                GetCollectionsLoadContainer(filterKey).GetEndOfCollection() || 
                GetCollectionsLoadContainer(filterKey).RunningTaskCount() > 0)
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
                        filter.Sort.Skip = batch.Skip;
                        filter.Sort.Take = batch.Take;
                        tasks.Add(GetTask(model, filter));
                        GetCollectionsLoadContainer(filterKey).AddRunningTask();
                    }
                }
            }

            while (tasks.Any())
            {
                var completedTask = await Task.WhenAny(tasks).ConfigureAwait(false);
                GetCollectionsLoadContainer(filterKey).RemoveRunningTask();
                PopToList(filterKey, completedTask.Result, dataLoading, singleRequest);
                tasks.Remove(completedTask);
            }
        }

        protected abstract Task<List<TViewModel>> GetTask(TModel model, FilterModel filter);

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

        protected virtual void OnCollectionFetched(List<TViewModel> models)
        {
            if (models.Any())
            {
                CollectionFetchedEvent?.Invoke(this, new CollectionFetchedEventArgs<TViewModel>(models));
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
