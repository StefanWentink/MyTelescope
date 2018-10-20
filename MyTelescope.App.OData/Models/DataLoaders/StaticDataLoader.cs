namespace MyTelescope.App.OData.Models.DataLoader
{
    using SWE.Model.Interfaces;
    using MyTelescope.Utilities.Models.Filter;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ViewModels.Interfaces;
    using SWE.OData.Interfaces;
    using System;
    using System.Threading;
    using SWE.Http.Interfaces;
    using MyTelescope.Data.Loader.Interfaces;
    using MyTelescope.App.Utilities.Interfaces;

    public abstract class StaticDataLoader<TView, T> :
        BaseDataLoader<TView, T>,
        IStaticDataLoader<TView, T>
        where TView : class, IBaseViewModel
        where T : class, IKey, new()
    {
        protected StaticDataLoader(IBatchContainer batchContainer)
            : base(batchContainer)
        {
        }

        protected override async Task<(int requestedCount, List<TView> items)> GetTask(
            T model,
            int requestedCount,
            CancellationToken cancellationToken,
            ISecurityToken securityToken,
            IODataBuilder<T, Guid> filter)
        {
            var filterKey = filter.BuilderKey();
            GetCollectionsLoadContainer(filterKey).SetEndOfCollection();
            return (requestedCount, await GetData(model).ConfigureAwait(false));
        }

        protected abstract Task<List<TView>> GetData(T model);
    }
}