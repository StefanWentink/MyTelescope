namespace MyTelescope.Test.SolarSystem.Extensions
{
    using System;
    using System.Linq;
    using Base;
    using MyTelescope.SolarSystem.Constants;
    using MyTelescope.SolarSystem.Enums;
    using MyTelescope.SolarSystem.Extensions;
    using MyTelescope.SolarSystem.Models.Keplerian;
    using MyTelescope.Utilities.Helpers;
    using Xunit;

    public class KeplerianModelExtensionsTest : IClassFixture<CustomFixture>
    {
        [Fact]
        public void GetKeplerianMeanAnomaly360Test()
        {
            var jupiter = KeplerianValueConstants.PlanetKeplerianModels.Single(x => x.SolarSystemObject == CelestialObject.Jupiter);
            var earth = KeplerianValueConstants.PlanetKeplerianModels.Single(x => x.SolarSystemObject == CelestialObject.Earth);

            var referenceDate = new DateTimeOffset(2004, 1, 1, 0, 0, 0, TimeSpan.Zero);

            var jupiterDateModel = jupiter.GetKeplerianDateModel(referenceDate);
            var earthDateModel = earth.GetKeplerianDateModel(referenceDate);

            var jupiterMeanAnomaly = jupiterDateModel.Values.GetMeanAnomaly360();
            var earthMeanAnomaly = earthDateModel.Values.GetMeanAnomaly360();

            Assert.True(141.0518.EqualsWithinTolerance(jupiterMeanAnomaly.Degrees, 3));
            Assert.True(357.5098.EqualsWithinTolerance(earthMeanAnomaly.Degrees, 3));
        }

        [Fact]
        public void GetKeplerianMeanAnomalyAroundZeroTest()
        {
            var jupiter = KeplerianValueConstants.PlanetKeplerianModels.Single(x => x.SolarSystemObject == CelestialObject.Jupiter);
            var earth = KeplerianValueConstants.PlanetKeplerianModels.Single(x => x.SolarSystemObject == CelestialObject.Earth);

            var referenceDate = new DateTimeOffset(2004, 1, 1, 0, 0, 0, TimeSpan.Zero);

            var jupiterDateModel = jupiter.GetKeplerianDateModel(referenceDate);
            var earthDateModel = earth.GetKeplerianDateModel(referenceDate);

            var jupiterMeanAnomaly = jupiterDateModel.Values.GetMeanAnomalyAroundZero();
            var earthMeanAnomaly = earthDateModel.Values.GetMeanAnomalyAroundZero();

            Assert.True((-38.9482).EqualsWithinTolerance(jupiterMeanAnomaly.Degrees, 3));
            Assert.True(177.5098.EqualsWithinTolerance(earthMeanAnomaly.Degrees, 3));
        }

        [Fact]
        public void GetKeplerianCalculateTest()
        {
            var jupiter = KeplerianValueConstants.PlanetKeplerianModels.Single(x => x.SolarSystemObject == CelestialObject.Jupiter);
            var earth = KeplerianValueConstants.PlanetKeplerianModels.Single(x => x.SolarSystemObject == CelestialObject.Earth);

            var referenceDate = new DateTimeOffset(2004, 1, 1, 0, 0, 0, TimeSpan.Zero);

            var jupiterDateModel = jupiter.GetKeplerianDateModel(referenceDate);
            var earthDateModel = earth.GetKeplerianDateModel(referenceDate);

            var jupiterCalculate = new KeplerianCalculationModel(jupiterDateModel);
            var earthCalculate = new KeplerianCalculationModel(earthDateModel);

            var jupiterX = jupiterCalculate.Location.X;
            var jupiterY = jupiterCalculate.Location.Y;
            var jupiterZ = jupiterCalculate.Location.Z;

            var earthX = earthCalculate.Location.X;
            var earthY = earthCalculate.Location.Y;
            var earthZ = earthCalculate.Location.Z;

            Assert.True(jupiterX.EqualsWithinTolerance(-4.9852, 4));
            Assert.True(jupiterY.EqualsWithinTolerance(2.0689, 4));
            Assert.True(jupiterZ.EqualsWithinTolerance(0.1030, 4));

            Assert.True(earthX.EqualsWithinTolerance(-0.1779, 4));
            Assert.True(earthY.EqualsWithinTolerance(0.9670, 4));
            Assert.True(earthZ.EqualsWithinTolerance(0, 4));
        }

        [Fact]
        public void GetKeplerianCalculationTest()
        {
            var jupiter = KeplerianValueConstants.PlanetKeplerianModels.Single(x => x.SolarSystemObject == CelestialObject.Jupiter);
            var earth = KeplerianValueConstants.PlanetKeplerianModels.Single(x => x.SolarSystemObject == CelestialObject.Earth);

            var referenceDate = new DateTimeOffset(2004, 1, 1, 0, 0, 0, TimeSpan.Zero);

            var jupiterDateModel = jupiter.GetKeplerianDateModel(referenceDate);
            var earthDateModel = earth.GetKeplerianDateModel(referenceDate);

            var jupiterCalculate = new KeplerianCalculationModel(jupiterDateModel);
            var earthCalculate = new KeplerianCalculationModel(earthDateModel);

            jupiterCalculate.SetEarthPosition(earthCalculate.Location, earthCalculate.CentricDistance);

            var actualLargeDelta = jupiterCalculate.LargeDelta;
            var actualEclipticLongitude = jupiterCalculate.EclipticLongitude;
            var actualEclipticLatitude = jupiterCalculate.EclipticLatitude;
            var actualDeclination = jupiterCalculate.Declination;
            var actualRightAscension = jupiterCalculate.RightAscension;
            
            var expectedLargeDelta = 4.933;
            var expectedEclipticLongitude = 167.0899;
            var expectedEclipticLatitude = 1.1969;
            var expectedDeclination = 6.2012;
            var expectedRightAscension = 168.5924;

            Assert.True(expectedLargeDelta.EqualsWithinTolerance(actualLargeDelta, 2));
            Assert.True(expectedEclipticLongitude.EqualsWithinTolerance(actualEclipticLongitude, 2));
            Assert.True(expectedEclipticLatitude.EqualsWithinTolerance(actualEclipticLatitude, 2));
            Assert.True(expectedDeclination.EqualsWithinTolerance(actualDeclination, 2));
            Assert.True(expectedRightAscension.EqualsWithinTolerance(actualRightAscension, 2));
        }
    }
}
