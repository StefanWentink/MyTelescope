﻿namespace MyTelescope.App.DataLayer.Models.DataLoader
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using SolarSystem.Helpers.Seeder;
    using SolarSystem.Models.CelestialObject;
    using ViewModels.Models.Item;

    public class PlanetDataLoader : StaticDataLoader<CelestialObjectViewModel, CelestialObjectModel>
    {
        protected override Task<List<CelestialObjectViewModel>> GetData(CelestialObjectModel model)
        {
            return Task.Run(() => CelestialObjectSeedHelper.GetPlanets().Select(x => new CelestialObjectViewModel(x)).ToList());
        }
    }
}