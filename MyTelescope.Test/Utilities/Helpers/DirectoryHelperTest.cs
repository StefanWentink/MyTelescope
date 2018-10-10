namespace MyTelescope.Test.Utilities.Helpers
{
    using Base;
    using MyTelescope.Utilities.Helpers;
    using Xunit;

    public class DistanceHelperTest : IClassFixture<CustomFixture>
    {
        [Fact]
        public void AstronomicalUnitsToKilometers()
        {
            double expected = 149597870.69;
            var actual = DistanceHelper.AstronomicalUnitsToKilometers(1);
            Assert.Equal(expected, actual);

            actual = DistanceHelper.AstronomicalUnitsToKilometers(2);
            Assert.Equal(expected * 2, actual);
        }

        [Fact]
        public void KilometersToAstronomicalUnits()
        {
            double expected = 1;
            var actual = DistanceHelper.KilometersToAstronomicalUnits(149597870.69);
            Assert.Equal(expected, actual);

            actual = DistanceHelper.KilometersToAstronomicalUnits(149597870.69 * 2);
            Assert.Equal(expected * 2, actual);
        }

        [Fact]
        public void AstronomicalUnitsToMillionKilometers()
        {
            double expected = 149.59787069;
            var actual = DistanceHelper.AstronomicalUnitsToMillionKilometers(1);
            Assert.Equal(expected, actual);

            actual = DistanceHelper.AstronomicalUnitsToMillionKilometers(2);
            Assert.Equal(expected * 2, actual);
        }

        [Fact]
        public void MillionKilometersToAstronomicalUnits()
        {
            double expected = 1;
            var actual = DistanceHelper.MillionKilometersToAstronomicalUnits(149.59787069);
            Assert.Equal(expected, actual);

            actual = DistanceHelper.MillionKilometersToAstronomicalUnits(149.59787069 * 2);
            Assert.Equal(expected * 2, actual);
        }
    }
}
