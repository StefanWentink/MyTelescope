namespace MyTelescope.Test.Utilities.Helpers
{
    using Base;
    using MyTelescope.Utilities.Helpers;
    using Xunit;

    public class IntHelperTest : IClassFixture<CustomFixture>
    {
        [Theory]
        [InlineData(null, true)]
        [InlineData("A", true)]
        [InlineData("0", true)]
        [InlineData("1.1", true)]
        [InlineData("1", false)]
        public void ToIntTest(object value, bool expected)
        {
            var actual = IntHelper.ToInt(value);
            var isDefaultResult = actual == default(int);

            if (!isDefaultResult)
            {
                actual = IntHelper.ToInt(value);
                isDefaultResult = actual == default(int);
            }

            Assert.Equal(expected, isDefaultResult);
        }

        [Theory]
        [InlineData(null, true)]
        [InlineData("A", true)]
        [InlineData("0", false)]
        [InlineData("1.1", true)]
        [InlineData("1", false)]
        public void ToIntTestOrNullTest(object value, bool expected)
        {
            var actual = IntHelper.ToIntOrNull(value);
            var isDefaultResult = actual == null;

            if (!isDefaultResult)
            {
                actual = IntHelper.ToIntOrNull(value);
                isDefaultResult = actual == null;
            }

            Assert.Equal(expected, isDefaultResult);
        }

        [Theory]
        [InlineData(null, true)]
        [InlineData("A", true)]
        [InlineData("0", true)]
        [InlineData("1.1", true)]
        [InlineData("1", false)]
        public void IntIsNullOrEmptyTest(object value, bool expected)
        {
            var actual = IntHelper.IntIsNullOrEmpty(value);
            Assert.Equal(expected, actual);
        }
    }
}