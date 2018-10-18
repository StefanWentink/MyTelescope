namespace MyTelescope.App.DataLayer.Models
{
    using SWE.Http.Enums;
    using MyTelescope.Utilities.Models.Filter;
    using Newtonsoft.Json;
    using SWE.Http.Models;

    public class MyTelescopeRequest : Request
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MyTelescopeRequest"/> class.
        /// <see cref="Request"/> for MyTelescope InfrastructureConfiguration
        /// </summary>
        /// <param name="apiRouteName">
        /// </param>
        /// <param name="apiActionName">
        /// </param>
        /// <param name="filter">
        /// </param>
        public MyTelescopeRequest(string apiRouteName, string apiActionName, FilterModel filter)
            : base(apiRouteName, apiActionName, JsonConvert.SerializeObject(filter), HttpVerb.Post)
        {
        }
    }
}