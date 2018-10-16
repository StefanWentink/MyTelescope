namespace MyTelescope.App.OData.Models.DataLoader
{
    using Interfaces;
    using SWE.Model.Interfaces;
    using MyTelescope.Utilities.Interfaces.Connector;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ViewModels.Interfaces;
    using SWE.OData.Interfaces;
    using System;
    using SWE.Http.Interfacess;
    using System.Threading;

    public abstract class HttpDataLoader<TView, T> :
        BaseDataLoader<TView, T>,
        IHttpDataLoader<TView, T>
        where TView : class, IBaseKeyViewModel<T>, new()
        where T : class, IKey, new()
    {
        protected IRepository Repository { get; set; }

        protected HttpDataLoader(IRepository repository)
        {
            Repository = repository;
        }

        protected override async Task<List<TView>> GetTask(
            T model,
            CancellationToken cancellationToken,
            IODataBuilder<T, Guid> filter)
        {
            var collection = await Repository.ReadAsync<T>(cancellationToken, filter.Build()).ConfigureAwait(false);
            return collection.Select(x => new TView { Model = x }).ToList();
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