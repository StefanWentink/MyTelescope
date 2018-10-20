namespace MyTelescope.OData
{
    using Microsoft.AspNet.OData.Extensions;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using MyTelescope.Api.DataLayer.Helpers.Di;
    using MyTelescope.Core.Utilities.Helpers;
    using MyTelescope.OData.Utilities;
    using Swashbuckle.AspNetCore.Swagger;
    using SWE.Swagger.DocumentFilters;
    using System.Linq;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            StartupHelper.Initialize();
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            DiHelper.ConfigureContextServices(services);

            services.AddOData();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            //services.AddMvcCore(options =>
            //{
            //    foreach (var outputFormatter in options.OutputFormatters.OfType<ODataOutputFormatter>().Where(_ => _.SupportedMediaTypes.Count == 0))
            //    {
            //        outputFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/prs.odatatestxx-odata"));
            //    }
            //    foreach (var inputFormatter in options.InputFormatters.OfType<ODataInputFormatter>().Where(_ => _.SupportedMediaTypes.Count == 0))
            //    {
            //        inputFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/prs.odatatestxx-odata"));
            //    }
            //});

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Info { Title = "MyTeleScope.OData", Version = "v1" });
                options.DocumentFilter<ODataDocumentFilter>("odata");
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            loggerFactory.AddConsole(Configuration.GetSection("Logging")); //log levels set in your configuration
            loggerFactory.AddDebug(); //does all log levels

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "MyTeleScope.OData");
                c.RoutePrefix = string.Empty;
            });

            app.UseMvc(
                routebuilder =>
                {
                    routebuilder.Select().Expand().Filter().OrderBy().MaxTop(100).Count();
                    routebuilder.MapODataServiceRoute("odata", "odata", ODataUtilities.GetODataConventionModelBuilder().GetEdmModel());
                    routebuilder.EnableDependencyInjection();
                });
        }
    }
}