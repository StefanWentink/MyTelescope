namespace MyTelescope.OData.Utilities
{
    using Microsoft.AspNet.OData.Builder;
    using MyTelescope.SolarSystem.Models.CelestialObject;

    public static class ODataUtilities
    {
        public static ODataConventionModelBuilder GetODataConventionModelBuilder()
        {
            var builder = new ODataConventionModelBuilder();

            builder.EntitySet<CelestialObjectTypeModel>(nameof(CelestialObjectTypeModel).Replace("Model", string.Empty));
            builder.EntitySet<CelestialObjectModel>(nameof(CelestialObjectModel).Replace("Model", string.Empty));
            builder.EntitySet<CelestialObjectPositionModel>(nameof(CelestialObjectPositionModel).Replace("Model", string.Empty));

            return builder;
        }
    }
}