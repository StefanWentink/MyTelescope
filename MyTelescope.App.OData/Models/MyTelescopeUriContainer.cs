namespace MyTelescope.App.OData.Models
{
    using SWE.Http.Constants;
    using SWE.Http.Models;

    public class MyTelescopeUriContainer : UriContainer
    {
        public MyTelescopeUriContainer()
            : base("https://localhost:44375", "odata", ExchangerConstants.ContentType, "/")
        {
        }
    }
}