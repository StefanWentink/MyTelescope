namespace MyTelescope.Core.Utilities.Helpers
{
    using System;
    using Microsoft.AspNetCore.Builder;
    using Models;

    public static class SslExtension
    {
        public static IApplicationBuilder UseSslEnforcement(this IApplicationBuilder app)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            return app.UseMiddleware<EnforceSslMiddleware>();
        }
    }
}
