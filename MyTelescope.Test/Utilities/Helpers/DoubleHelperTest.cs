namespace MyTelescope.Test.Utilities.Helpers
{
    using Base;
    using MyTelescope.Utilities.Helpers;
    using SWE.BasicType.Utilities;
    using Xunit;

    public class DoubleHelperTest : IClassFixture<CustomFixture>
    {
        [Theory]
        [InlineData(null, true)]
        [InlineData("A", true)]
        [InlineData("0", true)]
        [InlineData("1.1", false)]
        [InlineData("1", false)]
        [InlineData(2, false)]
        [InlineData(2.1, false)]
        public void ToDoubleTest(object value, bool expected)
        {
            var actual = DoubleHelper.ToDouble(value);
            var isDefaultResult = CompareUtilities.EqualsWithinTolerance(actual, default(double), 6);

            if (!isDefaultResult)
            {
                actual = DoubleHelper.ToDouble(value);
                isDefaultResult = CompareUtilities.EqualsWithinTolerance(actual, default(double), 6);
            }

            Assert.Equal(expected, isDefaultResult);
        }

        [Theory]
        [InlineData(null, true)]
        [InlineData("A", true)]
        [InlineData("0", false)]
        [InlineData("1.1", false)]
        [InlineData("1", false)]
        [InlineData(2, false)]
        [InlineData(2.1, false)]
        public void ToDoubleTestOrNullTest(object value, bool expected)
        {
            var actual = DoubleHelper.ToDoubleOrNull(value);
            var isDefaultResult = actual == null;

            if (!isDefaultResult)
            {
                actual = DoubleHelper.ToDoubleOrNull(value);
                isDefaultResult = actual == null;
            }

            Assert.Equal(expected, isDefaultResult);
        }

        [Theory]
        [InlineData(null, true)]
        [InlineData("A", true)]
        [InlineData("0", true)]
        [InlineData("1.1", false)]
        [InlineData("1", false)]
        [InlineData(2, false)]
        [InlineData(2.1, false)]
        public void DoubleIsNullOrEmptyTest(object value, bool expected)
        {
            var actual = DoubleHelper.DoubleIsNullOrEmpty(value);
            Assert.Equal(expected, actual);
        }
    }
}