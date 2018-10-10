namespace MyTelescope.App.DataLayer.Models
{
    using System.Collections.Generic;
    using Enums;
    using Http;
    using Interfaces;
    using Newtonsoft.Json;
    using MyTelescope.Utilities.Helpers;
    using MyTelescope.Utilities.Models.Filter;

    public class DataTransponder : IDataTransponder
    {
        private ICrudDataExchanger<IRequestModel> Exchanger { get; }

        public DataTransponder(ICrudDataExchanger<IRequestModel> exchanger)
        {
            Exchanger = exchanger;
        }

        public IEnumerable<TModel> Read<TModel>(FilterModel filter)
            where TModel : class
        {
            return Exchange<TModel>(filter);
        }

        private List<TModel> Exchange<TModel>(FilterModel filter)
        {
            var modelName = ModelHelper.GetName(typeof(TModel).Name);
            var requestModel = CreateRequestModel(modelName, Exchanger.ReadAction, JsonConvert.SerializeObject(filter), HttpVerb.Post);
            var result = Exchanger.GetString(requestModel).Result;

            try
            {
                return JsonConvert.DeserializeObject<List<TModel>>(result);
            }
            catch (JsonSerializationException)
            {
                Utilities.Helpers.LogHelper.LogError("Deserialization of response failed");
                Utilities.Helpers.LogHelper.LogError($"Target type: {modelName}");
                Utilities.Helpers.LogHelper.LogError($"Failed request: [{requestModel.Verb}] {requestModel.ApiRouteName}/{requestModel.ApiActionName}");
                Utilities.Helpers.LogHelper.LogError($"Request content: {requestModel.Content}");
                Utilities.Helpers.LogHelper.LogError($"Response: {result}");

                throw;
            }
        }

        protected virtual HttpRequestModel CreateRequestModel(string apiRouteName, string apiActionName, string content, HttpVerb verb)
        {
            return new HttpRequestModel(apiRouteName, apiActionName, content, verb);
        }
    }
}
