namespace MyTelescope.Test.SolarSystem.Extensions
{
    using Base;
    using MyTelescope.SolarSystem.Enums;
    using MyTelescope.SolarSystem.Extensions;
    using MyTelescope.SolarSystem.Models.CelestialObject;
    using MyTelescope.Utilities.Helpers;
    using MyTelescope.Utilities.Models;
    using SWE.BasicType.Utilities;
    using System;
    using System.Linq;
    using Xunit;

    public class CelestialObjectPositionModelExtensionsTest : IClassFixture<CustomFixture>
    {
        private static readonly CelestialObjectPosition EarthPosition = new CelestialObjectPosition(
            Guid.Empty,
            new DateTimeOffset(2017, 2, 2, 0, 0, 0, TimeSpan.FromHours(1)))
        {
            MeanAnomaly = 357.009
        };

        private static readonly CelestialObjectPosition JupiterPosition = new CelestialObjectPosition(
            Guid.Empty,
            new DateTimeOffset(2017, 2, 2, 0, 0, 0, TimeSpan.FromHours(1)))
        {
            RightAscension = 170.120,
            Declination = 5.567
        };

        [Theory]
        [InlineData(Interval.Minute, 12)]
        [InlineData(Interval.Quarter, 12)]
        [InlineData(Interval.Hour, 12)]
        [InlineData(Interval.Minute, 6)]
        [InlineData(Interval.Quarter, 6)]
        [InlineData(Interval.Hour, 6)]
        public void GetEarthSiderealTimePerDayTest(Interval interval, int hourRange)
        {
            const double expected = 99.946;
            var actual = EarthPosition.GetEarthSiderealTimePerDay(interval, hourRange).ToList();

            var partsPerHour = (int)Math.Round((int)Interval.Hour / (double)interval);
            var actualCount = hourRange * 2 * partsPerHour;
            Assert.Equal(actualCount, actual.Count);
            var actualValue = actual.ToList()[(hourRange + 1) * partsPerHour].SideRealTime;
            Assert.True(CompareUtilities.EqualsWithinTolerance(expected, actualValue, 6));
        }

        [Fact]
        public void GetTransitTimeSpanTest()
        {
            var expected = new TimeSpan(0, 4, 20, 41, 760);
            var actual = JupiterPosition.GetTransitTimeSpan(EarthPosition, -5);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetRiseTransitSetTest()
        {
            var expectedTransitTimeSpan = new TimeSpan(0, 4, 20, 41, 760);
            var expectedRiseTimeSpan = new TimeSpan(0, 21, 52, 1, 760);
            var expectedSetTimeSpan = new TimeSpan(0, 10, 49, 21, 760);

            var actual = JupiterPosition.GetRiseTransitSet(EarthPosition, -5, 52);

            Assert.Equal(expectedTransitTimeSpan.Hours, actual.Transit.Hour);
            Assert.Equal(expectedTransitTimeSpan.Minutes, actual.Transit.Minute);
            Assert.Equal(expectedTransitTimeSpan.Seconds, actual.Transit.Second);

            Assert.Equal(expectedRiseTimeSpan.Hours, actual.Rise.Hour);
            Assert.Equal(expectedRiseTimeSpan.Minutes, actual.Rise.Minute);
            Assert.Equal(expectedRiseTimeSpan.Seconds, actual.Rise.Second);

            Assert.Equal(expectedSetTimeSpan.Hours, actual.Set.Hour);
            Assert.Equal(expectedSetTimeSpan.Minutes, actual.Set.Minute);
            Assert.Equal(expectedSetTimeSpan.Seconds, actual.Set.Second);
        }

        [Fact]
        public void GetSkyPositionTest()
        {
            var siderealTime = new SideRealTimeModel(new DateTimeOffset(2014, 1, 1, 0, 0, 0, TimeSpan.Zero), -65.174);
            var actual = CelestialObjectPositionExtensions.GetSkyPosition(5.567, 52, siderealTime.DateTimeOffset, siderealTime.SideRealTime);

            Assert.Equal(actual.DateTimeOffset, siderealTime.DateTimeOffset);
            Assert.True(CompareUtilities.EqualsWithinTolerance(19.495, actual.Heigth, 3));
            Assert.True(CompareUtilities.EqualsWithinTolerance((-73.383), actual.Azimuth, 3));
        }
    }
}