namespace MyTelescope.App.OData.Models.DataLoader
{
    using MyTelescope.App.Utilities.Interfaces;
    using SolarSystem.Models.CelestialObject;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ViewModels.Models.Item;

    public class PlanetDetailLoader : StaticDataLoader<PlanetDetailViewModel, CelestialObject>
    {
        public PlanetDetailLoader(IBatchContainer batchContainer)
            : base(batchContainer)
        {
        }

        protected override Task<List<PlanetDetailViewModel>> GetData(CelestialObject model)
        {
            return Task.Run(() => new PlanetViewModel(model).GetDetails().Select(x => new PlanetDetailViewModel(x)).ToList());
        }
    }
}