namespace MyTelescope.Test.SolarSystem.Helpers
{
    using System;
    using System.Linq;
    using Base;
    using MyTelescope.SolarSystem.Enums;
    using MyTelescope.SolarSystem.Helpers;
    using MyTelescope.Utilities.Helpers;
    using Xunit;

    public class SolarSystemObjectHelperTest : IClassFixture<CustomFixture>
    {
        [Fact]
        public void SolarSystemObjectHelperListTest()
        {
           var list = EnumHelper.GetValues<CelestialObject>();
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
            var list = CelestialObjectHelper.GetMainMoons(CelestialObject.Earth).ToList();
            Assert.Single(list);

            list = CelestialObjectHelper.GetAllMoons(CelestialObject.Earth).ToList();
            Assert.Single(list);

            list = CelestialObjectHelper.GetMainMoons(CelestialObject.Jupiter).ToList();
            Assert.Equal(4, list.Count);

            list = CelestialObjectHelper.GetAllMoons(CelestialObject.Jupiter).ToList();
            Assert.Equal(5, list.Count);
        }

        [Fact]
        public void SolarSystemObjectHelperMoonThrowsTest()
        {
            Assert.Throws<ArgumentException>(() => CelestialObjectHelper.GetMainMoons(CelestialObject.Moon));

            Assert.Throws<ArgumentException>(() => CelestialObjectHelper.GetAllMoons(CelestialObject.Amalthea));
        }

        [Fact]
        public void GetSolarSystemObjectType()
        {
            var value = CelestialObject.Sun.GetSolarSystemObjectType();
            Assert.Equal(CelestialObjectType.Star, value);

            value = CelestialObject.Mercury.GetSolarSystemObjectType();
            Assert.Equal(CelestialObjectType.Planet, value);
            value = CelestialObject.Neptune.GetSolarSystemObjectType();
            Assert.Equal(CelestialObjectType.Planet, value);

            value = CelestialObject.Moon.GetSolarSystemObjectType();
            Assert.Equal(CelestialObjectType.MajorMoon, value);
            value = CelestialObject.Io.GetSolarSystemObjectType();
            Assert.Equal(CelestialObjectType.MajorMoon, value);

            value = CelestialObject.Amalthea.GetSolarSystemObjectType();
            Assert.Equal(CelestialObjectType.MinorMoon, value);
        }
    }
}