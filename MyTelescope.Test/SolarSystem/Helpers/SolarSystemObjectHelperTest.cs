namespace MyTelescope.Test.SolarSystem.Helpers
{
    using Base;
    using MyTelescope.SolarSystem.Enums;
    using MyTelescope.SolarSystem.Helpers;
    using MyTelescope.Utilities.Helpers;
    using System;
    using System.Linq;
    using Xunit;

    public class SolarSystemObjectHelperTest : IClassFixture<CustomFixture>
    {
        [Fact]
        public void SolarSystemObjectHelperListTest()
        {
            var list = EnumHelper.GetValues<Celestial>();
            Assert.NotEmpty(list);
        }

        [Fact]
        public void SolarSystemObjectHelperPlanetTest()
        {
            var list = CelestialObjectHelper.Planets;
            Assert.Equal(9, list.Count);
        }

        [Fact]
        public void SolarSystemObjectHelperMoonTest()
        {
            var list = CelestialObjectHelper.GetMainMoons(Celestial.Earth).ToList();
            Assert.Single(list);

            list = CelestialObjectHelper.GetAllMoons(Celestial.Earth).ToList();
            Assert.Single(list);

            list = CelestialObjectHelper.GetMainMoons(Celestial.Jupiter).ToList();
            Assert.Equal(4, list.Count);

            list = CelestialObjectHelper.GetAllMoons(Celestial.Jupiter).ToList();
            Assert.Equal(5, list.Count);
        }

        [Fact]
        public void SolarSystemObjectHelperMoonThrowsTest()
        {
            Assert.Throws<ArgumentException>(() => CelestialObjectHelper.GetMainMoons(Celestial.Moon));

            Assert.Throws<ArgumentException>(() => CelestialObjectHelper.GetAllMoons(Celestial.Amalthea));
        }

        [Fact]
        public void GetSolarSystemObjectType()
        {
            var value = Celestial.Sun.GetSolarSystemObjectType();
            Assert.Equal(CelestialType.Star, value);

            value = Celestial.Mercury.GetSolarSystemObjectType();
            Assert.Equal(CelestialType.Planet, value);
            value = Celestial.Neptune.GetSolarSystemObjectType();
            Assert.Equal(CelestialType.Planet, value);

            value = Celestial.Moon.GetSolarSystemObjectType();
            Assert.Equal(CelestialType.MajorMoon, value);
            value = Celestial.Io.GetSolarSystemObjectType();
            Assert.Equal(CelestialType.MajorMoon, value);

            value = Celestial.Amalthea.GetSolarSystemObjectType();
            Assert.Equal(CelestialType.MinorMoon, value);
        }
    }
}