namespace MyTelescope.App.DataLayer.Models.DataLoader
{
    using MyTelescope.App.Utilities.Interfaces;
    using SolarSystem.Models.CelestialObject;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ViewModels.Models.Item;

    public class MoonDetailLoader : StaticDataLoader<MoonDetailViewModel, CelestialObject>
    {
        public MoonDetailLoader(IBatchContainer batchContainer)
            : base(batchContainer)
        {
        }

        protected override Task<List<MoonDetailViewModel>> GetData(CelestialObject model)
        {
            return Task.Run(() => new MoonViewModel(model).GetDetails().Select(x => new MoonDetailViewModel(x)).ToList());
        }
    }
}