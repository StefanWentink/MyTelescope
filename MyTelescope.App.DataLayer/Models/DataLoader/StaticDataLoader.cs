﻿namespace MyTelescope.App.DataLayer.Models.DataLoader
{
    using Interfaces;
    using SWE.Model.Interfaces;
    using MyTelescope.Utilities.Models.Filter;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ViewModels.Interfaces;

    public abstract class StaticDataLoader<TViewModel, TModel> :
        BaseDataLoader<TViewModel, TModel>,
        IStaticDataLoader<TViewModel, TModel>
        where TViewModel : class, IBaseViewModel
        where TModel : class, IKey, new()
    {
        protected override async Task<List<TViewModel>> GetTask(TModel model, FilterModel filter)
        {
            var filterKey = filter.Key;
            GetCollectionsLoadContainer(filterKey).SetEndOfCollection();
            return await GetData(model).ConfigureAwait(false);
        }

        protected abstract Task<List<TViewModel>> GetData(TModel model);
    }
}