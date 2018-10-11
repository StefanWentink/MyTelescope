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
            const string expectedMyTeleScopeApi = "http://localhost:51425/";
            const string expectedHockeyAppId = "6xx6xxxxxxxxxxxxxx77x21";

            const string rootConfigurationString = "{\r\n  \"Infrastructure\": {\r\n    \"MyTeleScopeApi\": \"" + expectedMyTeleScopeApi + "\"\r\n  }\r\n}";
            const string platformConfigurationString = "{\r\n  \"HockeyAppId\": \"" + expectedHockeyAppId + "\"\r\n}";

            var actual = ConfigHelper.DeserializeConfig(rootConfigurationString, platformConfigurationString);

            Assert.Equal(expectedMyTeleScopeApi, actual.Infrastructure.MyTeleScopeApi);
            Assert.Equal(expectedHockeyAppId, actual.Platform.HockeyAppId);
        }
    }
}