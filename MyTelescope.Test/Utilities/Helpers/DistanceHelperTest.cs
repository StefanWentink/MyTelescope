namespace MyTelescope.Test.Utilities.Helpers
{
    using Base;
    using MyTelescope.Utilities.Helpers;
    using Xunit;

    public class DirectoryHelperTest : IClassFixture<CustomFixture>
    {
        [Fact]
        public void Test()
        {
            const string baseUrl = "c:\\project\\SomeTest";
            const string binUrl = baseUrl + "\\Bin\\object\\bla";

            var actual = DirectoryHelper.GetCurrentMainDirectory(baseUrl);
            Assert.Equal(baseUrl, actual);

            actual = DirectoryHelper.GetCurrentMainDirectory(binUrl);
            Assert.Equal(baseUrl, actual);
        }
    }
}