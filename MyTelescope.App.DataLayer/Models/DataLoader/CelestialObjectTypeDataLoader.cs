﻿namespace MyTelescope.App.DataLayer.Models.DataLoader
{
    using MyTelescope.Utilities.Interfaces.Connector;
    using MyTelescope.Utilities.Models.Sort;
    using SolarSystem.Models.CelestialObject;
    using ViewModels.Models.Item;

    public class CelestialObjectTypeDataLoader : HttpDataLoader<CelestialObjectTypeViewModel, CelestialObjectType>
    {
        public CelestialObjectTypeDataLoader(IConnector<CelestialObjectType> connector)
            : base(connector)
        {
        }

        protected override SortModel GetSort()
        {
            return new SortModel(nameof(CelestialObjectType.Code), true);
        }
    }
}