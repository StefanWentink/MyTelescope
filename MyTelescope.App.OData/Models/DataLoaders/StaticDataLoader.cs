﻿namespace MyTelescope.App.OData.Models.DataLoader
{
    using Interfaces;
    using SWE.Model.Interfaces;
    using MyTelescope.Utilities.Models.Filter;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ViewModels.Interfaces;
    using SWE.OData.Interfaces;
    using System;

    public abstract class StaticDataLoader<TView, T> :
        BaseDataLoader<TView, T>,
        IStaticDataLoader<TView, T>
        where TView : class, IBaseViewModel
        where T : class, IKey, new()
    {
        protected override async Task<List<TView>> GetTask(T model, IODataBuilder<T, Guid> filter)
        {
            var filterKey = filter.BuilderKey();
            GetCollectionsLoadContainer(filterKey).SetEndOfCollection();
            return await GetData(model).ConfigureAwait(false);
        }

        protected abstract Task<List<TView>> GetData(T model);
    }
}