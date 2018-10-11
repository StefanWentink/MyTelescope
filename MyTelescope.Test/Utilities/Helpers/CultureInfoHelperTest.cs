namespace MyTelescope.Test.Utilities.Helpers
{
    using Base;
    using MyTelescope.Utilities.Helpers;
    using System;
    using Xunit;

    public class CultureInfoHelperTest : IClassFixture<CustomFixture>
    {
        [Theory]
        [InlineData(" a- ble", "a-ble")]
        [InlineData(" ab -cd ", "ab-cd")]
        [InlineData(" n l _ nl", "nl-nl")]
        [InlineData(" nl", "nl")]
        public void CleanCutureCodeTest(string value, string expected)
        {
            var actual = CultureInfoHelper.CleanCutureCode(value);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(" ")]
        [InlineData(" nld-be ")]
        [InlineData("n-BL")]
        [InlineData("dfj-")]
        public void CleanCutureCodeThrowsTest(string value)
        {
            Assert.Throws<ArgumentException>(() => CultureInfoHelper.CleanCutureCode(value));
        }

        [Theory]
        [InlineData("nlNLs", "nlNLs", "")]
        [InlineData("NL_nl", "nl", "NL")]
        [InlineData("nl-NL", "nl", "NL")]
        public void SplitCultureCodePartsTest(string value, string expectedLanguageCode, string expectedLocaleCode)
        {
            var (languageCode, localeCode) = CultureInfoHelper.SplitCultureCodeParts(value);
            Assert.Equal(expectedLanguageCode, languageCode);
            Assert.Equal(expectedLocaleCode, localeCode);
        }

        [Theory]
        [InlineData("", "", "")]
        [InlineData("nl", "", "nl")]
        [InlineData("nl", "nl", "nl-NL")]
        [InlineData("", "nl", "-NL")]
        public void ConcatCultureCodePartsTest(string languageCode, string localeCode, string expected)
        {
            var actual = CultureInfoHelper.ConcatCultureCodeParts(languageCode, localeCode);
            Assert.Equal(expected, actual);
        }
    }
}