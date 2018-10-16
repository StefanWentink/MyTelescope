namespace MyTelescope.App.OData.Models.DataLoader
{
    using SolarSystem.Helpers.Seeder;
    using SolarSystem.Models.CelestialObject;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ViewModels.Models.Item;

    public class PlanetDataLoader : StaticDataLoader<CelestialObjectViewModel, CelestialObject>
    {
        protected override Task<List<CelestialObjectViewModel>> GetData(CelestialObject model)
        {
            return Task.Run(() => CelestialObjectSeedHelper.GetPlanets().Select(x => new CelestialObjectViewModel(x)).ToList());
        }
    }
}