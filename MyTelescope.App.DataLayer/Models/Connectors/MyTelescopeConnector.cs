namespace MyTelescope.App.DataLayer.Models.Connectors
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Http;
    using Interfaces;
    using MyTelescope.Utilities.Helpers;
    using MyTelescope.Utilities.Interfaces.Connector;
    using MyTelescope.Utilities.Models.Filter;
    using Newtonsoft.Json;
    using Utilities.Helpers;

    public abstract class MyTelescopeConnector<TModel> : IConnector<TModel>
        where TModel : class
    {
        private ICrudDataExchanger<IRequestModel> Exchanger { get; }

        protected MyTelescopeConnector(ICrudDataExchanger<IRequestModel> exchanger)
        {
            Exchanger = exchanger;
        }

        public List<TModel> Read(FilterModel filter)
        {
            return Task.Run(() => ReadAsync(filter)).Result;
        }

        public async Task<List<TModel>> ReadAsync(FilterModel filter)
        {
            var name = ModelHelper.GetName(typeof(TModel).Name);
            var requestModel = new MyTelescopeRequestModel(name, Exchanger.ReadAction, filter);

            try
            {
                var content = await Exchanger.GetString(requestModel).ConfigureAwait(false);
                return JsonConvert.DeserializeObject<List<TModel>>(content);
            }
            catch (Exception exception)
            {
                LogHelper.LogError(exception);
                throw;
            }
        }

        public async Task<bool> CreateAsync(TModel model)
        {
            return await CreateAsync(new List<TModel> { model }).ConfigureAwait(false);
        }

        public async Task<bool> CreateAsync(List<TModel> models)
        {
            await Task.Delay(1);
            throw new System.NotImplementedException();
        }

        public async Task<bool> UpdateAsync(TModel model)
        {
            return await UpdateAsync(new List<TModel> { model }).ConfigureAwait(false);
        }

        public async Task<bool> UpdateAsync(List<TModel> models)
        {
            await Task.Delay(1);
            throw new System.NotImplementedException();
        }

        public async Task<bool> DeleteAsync(TModel model)
        {
            return await DeleteAsync(new List<TModel> { model }).ConfigureAwait(false);
        }

        public async Task<bool> DeleteAsync(List<TModel> models)
        {
            await Task.Delay(1);
            throw new System.NotImplementedException();
        }
    }
}
