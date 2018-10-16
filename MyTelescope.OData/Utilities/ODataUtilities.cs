namespace MyTelescope.OData.Utilities
{
    using Microsoft.AspNet.OData.Builder;
    using MyTelescope.SolarSystem.Models.CelestialObject;

    public static class ODataUtilities
    {
        public static ODataConventionModelBuilder GetODataConventionModelBuilder()
        {
            var builder = new ODataConventionModelBuilder();

            builder.EntitySet<CelestialObjectType>(nameof(CelestialObjectType));
            builder.EntitySet<CelestialObject>(nameof(CelestialObject));
            builder.EntitySet<CelestialObjectPosition>(nameof(CelestialObjectPosition));

            return builder;
        }
    }
}