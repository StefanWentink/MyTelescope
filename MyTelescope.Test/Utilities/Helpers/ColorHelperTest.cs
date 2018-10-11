namespace MyTelescope.Test.Utilities.Helpers
{
    using Base;
    using MyTelescope.Utilities.Helpers;
    using System;
    using Xunit;

    public class ColorHelperTest : IClassFixture<CustomFixture>
    {
        [Theory]
        [InlineData("#000000")]
        [InlineData("#FFFFFF")]
        [InlineData("#ABCDEF")]
        [InlineData("#987654")]
        public void GetColorFromHexTest(string value)
        {
            var actual = ColorHelper.GetColorFromHex(value);
            Assert.True(actual != default(System.Drawing.Color));
        }

        [Theory]
        [InlineData("#34343G")]     // G
        [InlineData("4343434")]     // 4
        [InlineData("#34343")]      // short
        [InlineData("#3434343")]    // long
        public void GetColorFromHexThrowsTest(string value)
        {
            Assert.Throws<ArgumentException>(() => ColorHelper.GetColorFromHex(value));
        }
    }
}