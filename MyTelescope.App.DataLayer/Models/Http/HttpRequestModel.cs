namespace MyTelescope.App.DataLayer.Models.Http
{
    using Enums;
    using Interfaces;

    public class HttpRequestModel : IRequestModel
    {
        public string ApiRouteName { get; set; }

        public string ApiActionName { get; set; }

        public string Content { get; set; }

        public HttpVerb Verb { get; set; }

        public HttpRequestModel(string apiRouteName, string apiActionName, string content, HttpVerb verb)
        {
            ApiRouteName = apiRouteName;
            ApiActionName = apiActionName;
            Content = content;
            Verb = verb;
        }
    }
}