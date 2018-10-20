namespace MyTelescope.App.OData.Models.DataLoader
{
    using MyTelescope.App.Utilities.Interfaces;
    using MyTelescope.Utilities.Models.Sort;
    using SolarSystem.Models.CelestialObject;
    using SWE.Http.Interfaces;
    using ViewModels.Models.Item;

    public class CelestialObjectTypeDataLoader : HttpDataLoader<CelestialObjectTypeViewModel, CelestialObjectType>
    {
        public CelestialObjectTypeDataLoader(IBatchContainer batchContainer, IRepository<CelestialObjectType> repository)
            : base(batchContainer, repository)
        {
        }

        protected override SortModel GetSort()
        {
            return new SortModel(nameof(CelestialObjectType.Code), true);
        }
    }
}