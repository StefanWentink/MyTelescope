namespace MyTelescope.Test
{
    using Base;
    using MyTelescope.Utilities.Helpers;
    using MyTelescope.Utilities.Models;
    using SWE.BasicType.Utilities;
    using Xunit;

    public class LocationModelTest : IClassFixture<CustomFixture>
    {
        [Fact]
        public void GetLocationDeltaTest()
        {
            var model = new LocationModel(5, -2, -1);
            var compare = new LocationModel(2, 2, -4);

            var actual = model.GetLocationDelta(compare);

            Assert.Equal(3, actual.X);
            Assert.Equal(-4, actual.Y);
            Assert.Equal(3, actual.Z);
        }

        [Fact]
        public void GetEclipticLongitudeTest()
        {
            var model = new LocationModel(-4.87477, 0.97081, 0.10478);
            var actual = model.GetEclipticLongitude();
            const double expected = 168.737;
            Assert.True(CompareUtilities.EqualsWithinTolerance(actual, expected, 3));
        }

        [Fact]
        public void GetEclipticLatitudeTest()
        {
            var model = new LocationModel(-4.87477, 0.97081, 0.10478);
            const double largeDelta = 4.97161;
            var actual = model.GetEclipticLatitude(largeDelta);
            const double expected = 1.208;
            Assert.True(CompareUtilities.EqualsWithinTolerance(actual, expected, 3));
        }

        [Fact]
        public void GetRightAscensionTest()
        {
            const double eclipticLatitude = 1.208;
            const double eclipticLongitude = 168.737;
            var angle = LocationHelper.GetAngle();
            var actual = LocationHelper.GetRightAscension(eclipticLatitude, eclipticLongitude, angle);
            const double expected = 170.120;
            Assert.True(CompareUtilities.EqualsWithinTolerance(actual, expected, 3));
        }

        [Fact]
        public void GetDeclinationTest()
        {
            const double eclipticLatitude = 1.208;
            const double eclipticLongitude = 168.737;
            var angle = LocationHelper.GetAngle();
            var actual = LocationHelper.GetDeclination(eclipticLatitude, eclipticLongitude, angle);
            const double expected = 5.567;
            Assert.True(CompareUtilities.EqualsWithinTolerance(actual, expected, 3));
        }
    }
}