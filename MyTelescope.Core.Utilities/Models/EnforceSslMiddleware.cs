namespace MyTelescope.Core.Utilities.Models
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;

    public class EnforceSslMiddleware
    {
        private readonly RequestDelegate _requestDelegate;

        public EnforceSslMiddleware(RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate;
        }

        public async Task Invoke(HttpContext context)
        {
            var req = context.Request;
            if (req.IsHttps == false)
            {
                var url = "https://" + req.Host + req.Path + req.QueryString;
                context.Response.Redirect(url, permanent: true);
            }
            else
            {
                await _requestDelegate(context).ConfigureAwait(false);
            }
        }
    }
}
