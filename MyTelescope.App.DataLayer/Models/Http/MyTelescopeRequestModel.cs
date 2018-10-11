namespace MyTelescope.App.DataLayer.Models.Http
{
    using Enums;
    using MyTelescope.Utilities.Models.Filter;
    using Newtonsoft.Json;

    public class MyTelescopeRequestModel : HttpRequestModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MyTelescopeRequestModel"/> class.
        /// <see cref="HttpRequestModel"/> for MyTelescope InfrastructureConfiguration
        /// </summary>
        /// <param name="apiRouteName">
        /// </param>
        /// <param name="apiActionName">
        /// </param>
        /// <param name="filter">
        /// </param>
        public MyTelescopeRequestModel(string apiRouteName, string apiActionName, FilterModel filter)
            : base(apiRouteName, apiActionName, JsonConvert.SerializeObject(filter), HttpVerb.Post)
        {
        }
    }
}