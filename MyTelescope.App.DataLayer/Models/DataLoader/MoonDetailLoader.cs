namespace MyTelescope.App.DataLayer.Models.DataLoader
{
    using SolarSystem.Models.CelestialObject;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ViewModels.Models.Item;

    public class MoonDetailLoader : StaticDataLoader<MoonDetailViewModel, CelestialObjectModel>
    {
        protected override Task<List<MoonDetailViewModel>> GetData(CelestialObjectModel model)
        {
            return Task.Run(() => new MoonViewModel(model).GetDetails().Select(x => new MoonDetailViewModel(x)).ToList());
        }
    }
}