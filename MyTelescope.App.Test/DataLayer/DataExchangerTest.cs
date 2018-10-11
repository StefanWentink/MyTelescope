namespace MyTelescope.App.Test.DataLayer
{
    using App.DataLayer.Interfaces;
    using App.DataLayer.Models.Http;
    using Moq;
    using MyTelescope.Utilities.Enums;
    using MyTelescope.Utilities.Models.Filter;
    using MyTelescope.Utilities.Models.Sort;
    using System.Net;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using Xunit;
    using CustomFixture = Base.CustomFixture;

    public class DataExchangerTest : IClassFixture<CustomFixture>
    {
        [Fact]
        public void DataExchangerGetStringTest()
        {
            const string expected = "Content";

            var httpContent = new StringContent(expected, Encoding.UTF8, "application/text");
            var dataExchanger = new Mock<MyTelescopeDataExchanger>();

            dataExchanger.Setup(x => x.RequestContent(It.IsAny<IRequestModel>())).Returns(
                Task.Run(() => new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = httpContent
                }));

            var filter = new FilterModel(
                new SortModel(new SortItemModel("Id", true), 2, 5),
                new FilterItemModel("Code", ColumnType.StringColumn, FilterType.Equal, "iets"));

            var requestModel = new MyTelescopeRequestModel("{0}/{1}/{2}", "Actie", filter);

            var actual = dataExchanger.Object.GetString(requestModel).Result;

            Assert.Equal(expected, actual);
        }
    }
}