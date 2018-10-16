namespace MyTelescope.Test.SolarSystem.Extensions
{
    using Base;
    using MyTelescope.SolarSystem.Constants;
    using MyTelescope.SolarSystem.Enums;
    using MyTelescope.SolarSystem.Extensions;
    using MyTelescope.SolarSystem.Models.Keplerian;
    using MyTelescope.Utilities.Helpers;
    using SWE.BasicType.Utilities;
    using System;
    using System.Linq;
    using Xunit;

    public class KeplerianModelExtensionsTest : IClassFixture<CustomFixture>
    {
        [Fact]
        public void GetKeplerianMeanAnomaly360Test()
        {
            var jupiter = KeplerianValueConstants.PlanetKeplerianModels.Single(x => x.SolarSystemObject == Celestial.Jupiter);
            var earth = KeplerianValueConstants.PlanetKeplerianModels.Single(x => x.SolarSystemObject == Celestial.Earth);

            var referenceDate = new DateTimeOffset(2004, 1, 1, 0, 0, 0, TimeSpan.Zero);

            var jupiterDateModel = jupiter.GetKeplerianDateModel(referenceDate);
            var earthDateModel = earth.GetKeplerianDateModel(referenceDate);

            var jupiterMeanAnomaly = jupiterDateModel.Values.GetMeanAnomaly360();
            var earthMeanAnomaly = earthDateModel.Values.GetMeanAnomaly360();

            Assert.True(CompareUtilities.EqualsWithinTolerance(141.0518, jupiterMeanAnomaly.Degrees, 3));
            Assert.True(CompareUtilities.EqualsWithinTolerance(357.5098, earthMeanAnomaly.Degrees, 3));
        }

        [Fact]
        public void GetKeplerianMeanAnomalyAroundZeroTest()
        {
            var jupiter = KeplerianValueConstants.PlanetKeplerianModels.Single(x => x.SolarSystemObject == Celestial.Jupiter);
            var earth = KeplerianValueConstants.PlanetKeplerianModels.Single(x => x.SolarSystemObject == Celestial.Earth);

            var referenceDate = new DateTimeOffset(2004, 1, 1, 0, 0, 0, TimeSpan.Zero);

            var jupiterDateModel = jupiter.GetKeplerianDateModel(referenceDate);
            var earthDateModel = earth.GetKeplerianDateModel(referenceDate);

            var jupiterMeanAnomaly = jupiterDateModel.Values.GetMeanAnomalyAroundZero();
            var earthMeanAnomaly = earthDateModel.Values.GetMeanAnomalyAroundZero();

            Assert.True(CompareUtilities.EqualsWithinTolerance((-38.9482), jupiterMeanAnomaly.Degrees, 3));
            Assert.True(CompareUtilities.EqualsWithinTolerance(177.5098, earthMeanAnomaly.Degrees, 3));
        }

        [Fact]
        public void GetKeplerianCalculateTest()
        {
            var jupiter = KeplerianValueConstants.PlanetKeplerianModels.Single(x => x.SolarSystemObject == Celestial.Jupiter);
            var earth = KeplerianValueConstants.PlanetKeplerianModels.Single(x => x.SolarSystemObject == Celestial.Earth);

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

            Assert.True(CompareUtilities.EqualsWithinTolerance(jupiterX, -4.9852, 4));
            Assert.True(CompareUtilities.EqualsWithinTolerance(jupiterY, 2.0689, 4));
            Assert.True(CompareUtilities.EqualsWithinTolerance(jupiterZ, 0.1030, 4));

            Assert.True(CompareUtilities.EqualsWithinTolerance(earthX, -0.1779, 4));
            Assert.True(CompareUtilities.EqualsWithinTolerance(earthY, 0.9670, 4));
            Assert.True(CompareUtilities.EqualsWithinTolerance(earthZ, 0, 4));
        }

        [Fact]
        public void GetKeplerianCalculationTest()
        {
            var jupiter = KeplerianValueConstants.PlanetKeplerianModels.Single(x => x.SolarSystemObject == Celestial.Jupiter);
            var earth = KeplerianValueConstants.PlanetKeplerianModels.Single(x => x.SolarSystemObject == Celestial.Earth);

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

            const double expectedLargeDelta = 4.933;
            const double expectedEclipticLongitude = 167.0899;
            const double expectedEclipticLatitude = 1.1969;
            const double expectedDeclination = 6.2012;
            const double expectedRightAscension = 168.5924;

            Assert.True(CompareUtilities.EqualsWithinTolerance(expectedLargeDelta, actualLargeDelta, 2));
            Assert.True(CompareUtilities.EqualsWithinTolerance(expectedEclipticLongitude, actualEclipticLongitude, 2));
            Assert.True(CompareUtilities.EqualsWithinTolerance(expectedEclipticLatitude, actualEclipticLatitude, 2));
            Assert.True(CompareUtilities.EqualsWithinTolerance(expectedDeclination, actualDeclination, 2));
            Assert.True(CompareUtilities.EqualsWithinTolerance(expectedRightAscension, actualRightAscension, 2));
        }
    }
}