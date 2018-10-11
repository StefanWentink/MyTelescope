namespace MyTelescope.Utilities.Models.Connector
{
    using Enums;
    using Filter;
    using Interfaces.Connector;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public abstract class Connector<TModel> : IConnector<TModel>
        where TModel : class
    {
        public List<TModel> Read(FilterModel filter)
        {
            return Task.Run(() => Get(filter)).Result;
        }

        public async Task<List<TModel>> ReadAsync(FilterModel filter)
        {
            return await Get(filter).ConfigureAwait(false);
        }

        protected abstract Task<List<TModel>> Get(FilterModel filter);

        public async Task<bool> CreateAsync(TModel model)
        {
            return await CreateAsync(new List<TModel> { model }).ConfigureAwait(false);
        }

        public async Task<bool> CreateAsync(List<TModel> models)
        {
            return await ProcessAsync(models, ProcessDirective.Create).ConfigureAwait(false);
        }

        public async Task<bool> UpdateAsync(TModel model)
        {
            return await UpdateAsync(new List<TModel> { model }).ConfigureAwait(false);
        }

        public async Task<bool> UpdateAsync(List<TModel> models)
        {
            return await ProcessAsync(models, ProcessDirective.Update).ConfigureAwait(false);
        }

        public async Task<bool> DeleteAsync(TModel model)
        {
            return await DeleteAsync(new List<TModel> { model }).ConfigureAwait(false);
        }

        public async Task<bool> DeleteAsync(List<TModel> models)
        {
            return await ProcessAsync(models, ProcessDirective.Delete).ConfigureAwait(false);
        }

        protected abstract Task<bool> ProcessAsync(List<TModel> models, ProcessDirective directive);
    }
}