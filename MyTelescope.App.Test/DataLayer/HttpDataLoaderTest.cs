namespace MyTelescope.App.Test.DataLayer
{
    using SWE.Http.Enums;
    using App.DataLayer.Models.DataLoader;
    using Base;
    using Moq;
    using MyTelescope.Utilities.Interfaces.Connector;
    using MyTelescope.Utilities.Models.Filter;
    using SolarSystem.Helpers.Seeder;
    using SolarSystem.Models.CelestialObject;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Xunit;
    using MyTelescope.App.Utilities.Models;

    public class HttpDataLoaderTest : IClassFixture<CustomFixture>
    {
        [Fact]
        public void HttpDataLoaderCelestialObjectReadTest()
        {
            var planetResult = CelestialObjectSeedHelper.GetPlanets();
            var emptyResult = new List<CelestialObject>();

            var connector = new Mock<IConnector<CelestialObject>>();

            connector.Setup(x => x.ReadAsync(It.Is<FilterModel>(f => f.Sort.Skip <= 0))).Returns(
                Task.Run(() => planetResult));

            connector.Setup(x => x.ReadAsync(It.Is<FilterModel>(f => f.Sort.Skip > 0))).Returns(
                Task.Run(() => emptyResult));

            var collectionFetchedCount = 0;
            var endOfCollectionCount = 0;

            var dataLoader = new CelestialObjectDataLoader(connector.Object, new BatchContainer());

            dataLoader.CollectionFetchedEvent += (sender, args) => collectionFetchedCount++;
            dataLoader.EndOfCollectionEvent += (sender, args) => endOfCollectionCount++;

            Task.Run(() => dataLoader.LoadAsync(DataLoading.Refresh, CelestialObjectSeedHelper.GetSun())).ContinueWith(t =>
            {
                Assert.Equal(1, collectionFetchedCount);
                Assert.Equal(1, endOfCollectionCount);
            });
        }
    }
}