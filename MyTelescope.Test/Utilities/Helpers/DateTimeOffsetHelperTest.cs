namespace MyTelescope.Test.Utilities.Helpers
{
    using Base;
    using MyTelescope.Utilities.Helpers;
    using System;
    using Xunit;

    public class DateTimeOffsetHelperTest : IClassFixture<CustomFixture>
    {
        [Theory]
        [InlineData(null, true)]
        [InlineData(360, true)]
        [InlineData("01-01-0001 00:00:00 +00:00", true)]
        [InlineData("11-11-2011", false)]
        [InlineData("13-11-2011", false)]
        [InlineData("11-13-2011", true)]
        public void ToDateTimeOffsetTest(object value, bool expected)
        {
            var actual = DateTimeOffsetHelper.ToDateTimeOffset(value);
            var isDefaultResult = actual == default(DateTimeOffset);

            if (!isDefaultResult)
            {
                actual = DateTimeOffsetHelper.ToDateTimeOffset(value);
                isDefaultResult = actual == default(DateTimeOffset);
            }

            Assert.Equal(expected, isDefaultResult);
        }

        [Theory]
        [InlineData(null, true)]
        [InlineData(360, true)]
        [InlineData("01-01-0001 00:00:00 +00:00", false)]
        [InlineData("11-11-2011", false)]
        [InlineData("13-11-2011", false)]
        [InlineData("11-13-2011", true)]
        public void ToDateTimeOffsetTestOrNullTest(object value, bool expected)
        {
            var actual = DateTimeOffsetHelper.ToDateTimeOffsetOrNull(value);
            var isDefaultResult = actual == null;

            if (!isDefaultResult)
            {
                actual = DateTimeOffsetHelper.ToDateTimeOffsetOrNull(value);
                isDefaultResult = actual == null;
            }

            Assert.Equal(expected, isDefaultResult);
        }

        [Theory]
        [InlineData(null, true)]
        [InlineData(360, true)]
        [InlineData("01-01-0001 00:00:00 +00:00", true)]
        [InlineData("11-11-2011", false)]
        [InlineData("13-11-2011", false)]
        [InlineData("11-13-2011", true)]
        public void DateTimeOffsetIsNullOrEmptyTest(object value, bool expected)
        {
            var actual = DateTimeOffsetHelper.DateTimeOffsetIsNullOrEmpty(value);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("01-01-0002")]
        [InlineData("11-11-2011")]
        [InlineData("13-11-2011")]
        [InlineData("11-05-2011")]
        public void GetClosestUtcZeroTimeTest(object value)
        {
            var dateTimeOffset = DateTimeOffsetHelper.ToDateTimeOffset(value);

            var actual = dateTimeOffset.DateTime.GetClosestUtcZeroTime();

            Assert.Equal(dateTimeOffset.Year, actual.Year);
            Assert.Equal(dateTimeOffset.Month, actual.Month);
            Assert.Equal(dateTimeOffset.Day, actual.Day);
            Assert.Equal(dateTimeOffset.Year, actual.Year);
            Assert.Equal(dateTimeOffset.Hour, actual.Hour);
            Assert.Equal(dateTimeOffset.Minute, actual.Minute);
            Assert.Equal(dateTimeOffset.Second, actual.Second);
        }
    }
}