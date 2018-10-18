namespace MyTelescope.App.DataLayer.Models
{
    using SWE.Http.Constants;
    using SWE.Http.Models;

    public class MyTelescopeUriContainer : UriContainer
    {
        public MyTelescopeUriContainer()
            : this("http://localhost:56823")
        {
        }

        public MyTelescopeUriContainer(string url)
            : base(url, ExchangerConstants.Format, ExchangerConstants.ContentType)
        {
        }

        public MyTelescopeUriContainer(string url, string apiFormat, string contentType)
            : base(url, apiFormat, contentType)
        {
        }
    }
}