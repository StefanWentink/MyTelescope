namespace MyTelescope.App.Test.DataLayer
{
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
    using SWE.Http.Models;
    using System.Threading;
    using SWE.Http.Models.Policies;
    using SWE.Http.Interfaces;

    public class DataExchangerTest : IClassFixture<CustomFixture>
    {
        [Fact]
        public void DataExchangerGetStringTest()
        {
            const string expected = "Content";

            var httpContent = new StringContent(expected, Encoding.UTF8, "application/text");
            var exchanger = new Mock<Exchanger>();

            exchanger.Setup(x => x.GetString(
                It.IsAny<CancellationToken>(),
                null,
                It.IsAny<TimeOutPolicy>(),
                It.IsAny<IRequest>())).Returns(
                Task.Run(() => expected)
                );

            var filter = new FilterModel(
                new SortModel(new SortItemModel("Id", true), 2, 5),
                new FilterItemModel("Code", ColumnType.StringColumn, FilterType.Equal, "iets"));

            var requestModel = new MyTelescopeRequest("{0}/{1}/{2}", "Actie", filter);

            var actual = exchanger.Object.GetString(new CancellationToken(), null, new TimeOutPolicy(200), requestModel).Result;

            Assert.Equal(expected, actual);
        }
    }
}