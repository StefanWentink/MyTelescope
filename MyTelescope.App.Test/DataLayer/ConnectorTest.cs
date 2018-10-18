namespace MyTelescope.App.Test.DataLayer
{
    using MyTelescope.Data.Loader.Interfaces;
    using Base;
    using Data;
    using Moq;
    using MyTelescope.Utilities.Enums;
    using MyTelescope.Utilities.Models.Filter;
    using MyTelescope.Utilities.Models.Sort;
    using Newtonsoft.Json;
    using System.Threading.Tasks;
    using Xunit;
    using SWE.Http.Interfaces;
    using System.Threading;
    using SWE.Http.Models.Policies;

    public class ConnectorTest : IClassFixture<CustomFixture>
    {
        [Fact]
        public void TelescopeConnectorCelestialObjectReadTest()
        {
            var planetContent = ResponseConstants.GetCelestialObjectPlanetContentString();
            var emptyContent = ResponseConstants.GetCelestialObjectEmptyContentString();

            var exchanger = new Mock<IExchanger>();

            exchanger.Setup(x => x.GetString(
                It.IsAny<CancellationToken>(),
                null,
                It.IsAny<TimeOutPolicy>(),
                It.Is<IRequest>(c => JsonConvert.DeserializeObject<FilterModel>(c.Content).Sort.Skip <= 0))).Returns(
                Task.Run(() => planetContent));

            exchanger.Setup(x => x.GetString(
                It.IsAny<CancellationToken>(),
                null,
                It.IsAny<TimeOutPolicy>(),
                It.Is<IRequest>(c => JsonConvert.DeserializeObject<FilterModel>(c.Content).Sort.Skip > 0))).Returns(
                Task.Run(() => emptyContent));

            var connector = new App.DataLayer.Connectors.CelestialObjectConnector(exchanger.Object);

            var filterPlanet = new FilterModel(
                new SortModel(new SortItemModel("Id", true), 0, 10),
                new FilterItemModel("Code", ColumnType.StringColumn, FilterType.Equal, "iets"));

            var actualPlanet = connector.ReadAsync(filterPlanet).Result;
            Assert.Equal(9, actualPlanet.Count);

            var filterEmpty = new FilterModel(
                new SortModel(new SortItemModel("Id", true), 10, 20),
                new FilterItemModel("Code", ColumnType.StringColumn, FilterType.Equal, "iets"));

            var actualEmpty = connector.ReadAsync(filterEmpty).Result;
            Assert.Empty(actualEmpty);
        }
    }
}