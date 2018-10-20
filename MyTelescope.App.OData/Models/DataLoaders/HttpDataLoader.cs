namespace MyTelescope.App.OData.Models.DataLoader
{
    using SWE.Model.Interfaces;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ViewModels.Interfaces;
    using SWE.OData.Interfaces;
    using System;
    using SWE.Http.Interfaces;
    using System.Threading;
    using MyTelescope.Data.Loader.Interfaces;
    using MyTelescope.App.Utilities.Interfaces;

    public abstract class HttpDataLoader<TView, T> :
        BaseDataLoader<TView, T>,
        IHttpDataLoader<TView, T>
        where TView : class, IBaseKeyViewModel<T>, new()
        where T : class, IKey, new()
    {
        protected IRepository<T> Repository { get; set; }

        protected HttpDataLoader(IBatchContainer batchContainer, IRepository<T> repository)
            : base(batchContainer)
        {
            Repository = repository;
        }

        protected override async Task<(int requestedCount, List<TView> items)> GetTask(
            T model,
            int requestedCount,
            CancellationToken cancellationToken,
            ISecurityToken securityToken,
            IODataBuilder<T, Guid> filter)
        {
            var collection = await Repository.ReadAsync(cancellationToken, securityToken, filter.Build()).ConfigureAwait(false);
            return (requestedCount, collection.Select(x => new TView { Model = x }).ToList());
        }

        ~HttpDataLoader()
        {
            if (Repository == null)
            {
                Repository = null;
            }
        }
    }
}