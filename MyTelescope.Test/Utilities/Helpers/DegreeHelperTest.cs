namespace MyTelescope.Test.Utilities.Helpers
{
    using Base;
    using MyTelescope.Utilities.Helpers;
    using SWE.BasicType.Utilities;
    using System;
    using Xunit;

    public class DegreeHelperTest : IClassFixture<CustomFixture>
    {
        [Theory]
        [InlineData(360, Math.PI * 2)]
        [InlineData(180, Math.PI)]
        [InlineData(0, 0)]
        public void DegreeToRadiansTest(double value, double expected)
        {
            var actual = DegreeHelper.DegreesToRadians(value);
            Assert.True(CompareUtilities.EqualsWithinTolerance(expected, actual, 6), $"expected {expected} and actual {actual} are not equal.");
        }

        [Theory]
        [InlineData(Math.PI * 2, 360)]
        [InlineData(Math.PI, 180)]
        [InlineData(0, 0)]
        public void RadiansToDegreesTest(double value, double expected)
        {
            var actual = DegreeHelper.RadiansToDegrees(value);
            Assert.True(CompareUtilities.EqualsWithinTolerance(expected, actual, 6), $"expected {expected} and actual {actual} are not equal.");
        }

        [Theory]
        [InlineData(3420, 180)]
        [InlineData(360.1, 0.1)]
        [InlineData(360, 0)]
        [InlineData(359.9, 359.9)]
        [InlineData(0, 0)]
        [InlineData(-0.1, 359.9)]
        public void Modulo360AbsoluteTest(double value, double expected)
        {
            var actual = DegreeHelper.Modulo360Absolute(value);
            Assert.True(CompareUtilities.EqualsWithinTolerance(expected, actual, 6), $"expected {expected} and actual {actual} are not equal.");
        }

        [Theory]
        [InlineData(3420, 0)]
        [InlineData(360.1, -179.9)]
        [InlineData(360, -180)]
        [InlineData(359.9, 179.9)]
        [InlineData(0, -180)]
        [InlineData(-0.1, 179.9)]
        public void Modulo360AroundZeroTest(double value, double expected)
        {
            var actual = DegreeHelper.Modulo360AroundZero(value);
            Assert.True(CompareUtilities.EqualsWithinTolerance(expected, actual, 6), $"expected {expected} and actual {actual} are not equal.");
        }

        [Theory]
        [InlineData(360)]
        [InlineData(188)]
        [InlineData(180)]
        [InlineData(179)]
        [InlineData(90)]
        [InlineData(257.175)]
        public void DegreeTimeSpanConversionTest(double value)
        {
            var actualTimeSpan = DegreeHelper.DegreeToTimeSpan(value);
            var expectedTimeSpan = new TimeSpan((int)Math.Floor(value / 15), (int)Math.Floor(value % 15) * 4, (int)Math.Floor((value % 0.25) * 240));
            Assert.Equal(expectedTimeSpan.Hours, actualTimeSpan.Hours);
            Assert.Equal(expectedTimeSpan.Minutes, actualTimeSpan.Minutes);
            Assert.Equal(expectedTimeSpan.Seconds, actualTimeSpan.Seconds);

            var actualDegrees = DegreeHelper.TimeSpanToDegree(actualTimeSpan);
            Assert.Equal(value, actualDegrees);
        }

        [Theory]
        [InlineData(257.175, 17, 8, 42)]
        public void DegreeToTimeTest(double value, int expectedHours, int expectedMinutes, int expectedSeconds)
        {
            var actualTimeSpan = DegreeHelper.DegreeToTimeSpan(value);
            Assert.Equal(expectedHours, actualTimeSpan.Hours);
            Assert.Equal(expectedMinutes, actualTimeSpan.Minutes);
            Assert.Equal(expectedSeconds, actualTimeSpan.Seconds);
        }

        [Theory]
        [InlineData(17, 8, 42, 257.175)]
        public void TimeToDegreeTest(int hours, int minutes, int seconds, double expected)
        {
            var actualTimeSpan = DegreeHelper.TimeSpanToDegree(new TimeSpan(hours, minutes, seconds));
            Assert.Equal(expected, actualTimeSpan);
        }
    }
}