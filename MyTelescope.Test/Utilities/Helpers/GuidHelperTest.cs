namespace MyTelescope.Test.Utilities.Helpers
{
    using System;
    using Base;
    using MyTelescope.Utilities.Helpers;
    using Xunit;

    public class GuidHelperTest : IClassFixture<CustomFixture>
    {
        [Theory]
        [InlineData(null, true)]
        [InlineData(360, true)]
        [InlineData("00000000-0000-0000-0000-000000000000", true)]
        [InlineData("00000000-0000-0000-0000-000000000001", false)]
        public void ToGuidTest(object value, bool expected)
        {
            var actual = GuidHelper.ToGuid(value);
            var isDefaultResult = actual == default(Guid);

            if (!isDefaultResult)
            {
                actual = GuidHelper.ToGuid(value);
                isDefaultResult = actual == default(Guid);
            }

            Assert.Equal(expected, isDefaultResult);
        }

        [Theory]
        [InlineData(null, true)]
        [InlineData(360, true)]
        [InlineData("00000000-0000-0000-0000-000000000000", false)]
        [InlineData("00000000-0000-0000-0000-000000000001", false)]
        public void ToGuidTestOrNullTest(object value, bool expected)
        {
            var actual = GuidHelper.ToGuidOrNull(value);
            var isDefaultResult = actual == null;

            if (!isDefaultResult)
            {
                actual = GuidHelper.ToGuidOrNull(value);
                isDefaultResult = actual == null;
            }

            Assert.Equal(expected, isDefaultResult);
        }

        [Theory]
        [InlineData(null, true)]
        [InlineData(360, true)]
        [InlineData("00000000-0000-0000-0000-000000000000", true)]
        [InlineData("00000000-0000-0000-0000-000000000001", false)]
        public void GuidIsNullOrEmptyTest(object value, bool expected)
        {
            var actual = GuidHelper.GuidIsNullOrEmpty(value);
            Assert.Equal(expected, actual);
        }
    }
}