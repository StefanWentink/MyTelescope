namespace MyTelescope.App.OData.Models
{
    using SWE.Http.Models;

    public class MyTelescopeUriContainer : UriContainer
    {
        public MyTelescopeUriContainer()
            : base("http://localhost:58138", "odata")
        {
        }
    }
}