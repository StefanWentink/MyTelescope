namespace MyTelescope.App.Test.DataLayer
{
    using System.Net;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using App.DataLayer.Interfaces;
    using App.DataLayer.Models.Http;
    using Base;
    using Data;
    using Moq;
    using MyTelescope.Utilities.Enums;
    using MyTelescope.Utilities.Models.Filter;
    using MyTelescope.Utilities.Models.Sort;
    using Newtonsoft.Json;
    using Xunit;

    public class ConnectorTest : IClassFixture<CustomFixture>
    {
        [Fact]
        public void TelescopeConnectorCelestialObjectReadTest()
        {
            var planetContent = ResponseConstants.GetCelestialObjectPlanetContentString();
            var emptyContent = ResponseConstants.GetCelestialObjectEmptyContentString();

            var dataExchanger = new Mock<ICrudDataExchanger<IRequestModel>>();

            dataExchanger.Setup(x => x.GetString(It.Is<IRequestModel>(c => JsonConvert.DeserializeObject<FilterModel>(c.Content).Sort.Skip <= 0))).Returns(
                Task.Run(() => planetContent));

            dataExchanger.Setup(x => x.GetString(It.Is<IRequestModel>(c => JsonConvert.DeserializeObject<FilterModel>(c.Content).Sort.Skip > 0))).Returns(
                Task.Run(() => emptyContent));

            var connector = new App.DataLayer.Models.Connectors.CelestialObjectConnector(dataExchanger.Object);
            
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
