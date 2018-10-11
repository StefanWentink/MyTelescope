namespace MyTelescope.App.Test.Data
{
    using Newtonsoft.Json;
    using SolarSystem.Helpers.Seeder;
    using SolarSystem.Models.CelestialObject;
    using System.Collections.Generic;

    internal static class ResponseConstants
    {
        internal static string GetCelestialObjectPlanetContentString()
        {
            return JsonConvert.SerializeObject(CelestialObjectSeedHelper.GetPlanets());
        }

        internal static string GetCelestialObjectEmptyContentString()
        {
            return JsonConvert.SerializeObject(new List<CelestialObjectModel>());
        }
    }
}