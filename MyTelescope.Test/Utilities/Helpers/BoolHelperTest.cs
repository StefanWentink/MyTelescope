namespace MyTelescope.Test.Utilities.Helpers
{
    using Base;
    using MyTelescope.Utilities.Helpers;
    using Xunit;

    public class BoolHelperTest : IClassFixture<CustomFixture>
    {
        [Theory]
        [InlineData(null, true)]
        [InlineData(false, true)]
        [InlineData(true, false)]
        [InlineData("1", true)]
        [InlineData("false", true)]
        [InlineData("true", false)]
        public void ToBoolTest(object value, bool expected)
        {
            var actual = BoolHelper.ToBool(value);
            var isDefaultResult = actual == default(bool);

            Assert.Equal(expected, isDefaultResult);
        }

        [Theory]
        [InlineData(null, true)]
        [InlineData(false, false)]
        [InlineData(true, false)]
        [InlineData("1", true)]
        [InlineData("false", false)]
        [InlineData("true", false)]
        public void ToBoolTestOrNullTest(object value, bool expected)
        {
            var actual = BoolHelper.ToBoolOrNull(value);
            var isDefaultResult = actual == null;

            Assert.Equal(expected, isDefaultResult);
        }

        [Theory]
        [InlineData(null, true)]
        [InlineData(false, true)]
        [InlineData(true, false)]
        public void BoolIsNullOrEmptyTest(object value, bool expected)
        {
            var actual = BoolHelper.BoolIsNullOrEmpty(value);
            Assert.Equal(expected, actual);
        }
    }
}