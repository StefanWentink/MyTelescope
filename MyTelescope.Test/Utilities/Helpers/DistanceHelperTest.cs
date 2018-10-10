namespace MyTelescope.Test.Utilities.Helpers
{
    using System.Collections.Generic;
    using System.Linq;
    using Base;
    using Data;
    using MyTelescope.Utilities.Helpers;
    using Xunit;

    public class DirectoryHelperTest : IClassFixture<CustomFixture>
    {
        [Fact]
        public void Test()
        {
            var baseUrl = "c:\\project\\SomeTest";
            var binUrl = baseUrl + "\\Bin\\object\\bla";

            var actual = DirectoryHelper.GetCurrentMainDirectory(baseUrl);
            Assert.Equal(baseUrl, actual);

            actual = DirectoryHelper.GetCurrentMainDirectory(binUrl);
            Assert.Equal(baseUrl, actual);
        }
    }
}
