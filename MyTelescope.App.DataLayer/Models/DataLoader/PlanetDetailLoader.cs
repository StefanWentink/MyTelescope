﻿namespace MyTelescope.App.DataLayer.Models.DataLoader
{
    using SolarSystem.Models.CelestialObject;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ViewModels.Models.Item;

    public class PlanetDetailLoader : StaticDataLoader<PlanetDetailViewModel, CelestialObject>
    {
        protected override Task<List<PlanetDetailViewModel>> GetData(CelestialObject model)
        {
            return Task.Run(() => new PlanetViewModel(model).GetDetails().Select(x => new PlanetDetailViewModel(x)).ToList());
        }
    }
}