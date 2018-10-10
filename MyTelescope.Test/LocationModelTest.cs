namespace MyTelescope.Test
{
    using Base;
    using MyTelescope.Utilities.Helpers;
    using MyTelescope.Utilities.Models;
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
            var expected = 168.737;
            Assert.True(actual.EqualsWithinTolerance(expected, 3));
        }

        [Fact]
        public void GetEclipticLatitudeTest()
        {
            var model = new LocationModel(-4.87477, 0.97081, 0.10478);
            var largeDelta = 4.97161;
            var actual = model.GetEclipticLatitude(largeDelta);
            var expected = 1.208;
            Assert.True(actual.EqualsWithinTolerance(expected, 3));
        }

        [Fact]
        public void GetRightAscensionTest()
        {
            var eclipticLatitude = 1.208;
            var eclipticLongitude = 168.737;
            var angle = LocationHelper.GetAngle();
            var actual = LocationHelper.GetRightAscension(eclipticLatitude, eclipticLongitude, angle);
            var expected = 170.120;
            Assert.True(actual.EqualsWithinTolerance(expected, 3));
        }

        [Fact]
        public void GetDeclinationTest()
        {
            var eclipticLatitude = 1.208;
            var eclipticLongitude = 168.737;
            var angle = LocationHelper.GetAngle();
            var actual = LocationHelper.GetDeclination(eclipticLatitude, eclipticLongitude, angle);
            var expected = 5.567;
            Assert.True(actual.EqualsWithinTolerance(expected, 3));
        }
    }
}
