namespace MyTelescope.App.DataLayer.Models.DataLoader
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using SolarSystem.Models.CelestialObject;
    using ViewModels.Models.Item;

    public class PlanetDetailLoader : StaticDataLoader<PlanetDetailViewModel, CelestialObjectModel>
    {
        protected override Task<List<PlanetDetailViewModel>> GetData(CelestialObjectModel model)
        {
            return Task.Run(() => { return new PlanetViewModel(model).GetDetails().Select(x => new PlanetDetailViewModel(x)).ToList(); });
        }
    }
}
