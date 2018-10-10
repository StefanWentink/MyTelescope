namespace MyTelescope.App.Test.Utilities
{
    using App.Utilities.Helpers;
    using Base;
    using Xunit;

    public class ConfigHelperTest : IClassFixture<CustomFixture>
    {

        [Fact]
        public void DeserializeConfigTest()
        {
            var expectedMyTeleScopeApi = "http://localhost:51425/";
            var expectedHockeyAppId = "6xx6xxxxxxxxxxxxxx77x21";

            var rootConfigurationString = "{\r\n  \"Infrastructure\": {\r\n    \"MyTeleScopeApi\": \"" + expectedMyTeleScopeApi + "\"\r\n  }\r\n}";
            var platformConfigurationString = "{\r\n  \"HockeyAppId\": \"" + expectedHockeyAppId + "\"\r\n}";

            var actual = ConfigHelper.DeserializeConfig(rootConfigurationString, platformConfigurationString);

            Assert.Equal(expectedMyTeleScopeApi, actual.Infrastructure.MyTeleScopeApi);
            Assert.Equal(expectedHockeyAppId, actual.Platform.HockeyAppId);
        }
    }
}
