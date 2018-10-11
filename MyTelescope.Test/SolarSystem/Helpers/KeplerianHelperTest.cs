namespace MyTelescope.Test.SolarSystem.Helpers
{
    using Base;
    using MyTelescope.SolarSystem.Helpers;
    using MyTelescope.Utilities.Helpers;
    using MyTelescope.Utilities.Models;
    using SWE.BasicType.Utilities;
    using System;
    using Xunit;

    public class KeplerianHelperTest : IClassFixture<CustomFixture>
    {
        [Fact]
        public void KeplerianHelperGetJ2000CenturyFactorTest()
        {
            var value = KeplerianHelper.GetJ2000CenturyFactor(new DateTimeOffset(1900, 1, 1, 0, 0, 0, TimeSpan.Zero));
            Assert.True(CompareUtilities.EqualsWithinTolerance(value, -1, 5));

            value = KeplerianHelper.GetJ2000CenturyFactor(new DateTimeOffset(2000, 1, 1, 0, 0, 0, TimeSpan.Zero));
            Assert.Equal(0, value);

            value = KeplerianHelper.GetJ2000CenturyFactor(new DateTimeOffset(2100, 1, 1, 0, 0, 0, TimeSpan.Zero));
            Assert.True(CompareUtilities.EqualsWithinTolerance(value, 1, 4));
        }

        [Fact]
        public void KeplerianHelperTEst()
        {
            const double centricDistance = 5.40406;
            var ascendingNodeLongitude = new DegreeModel(100.464);
            var perihelionOmega = new DegreeModel(273.867);
            var trueAnomaly = new DegreeModel(144.637);
            var inclination = new DegreeModel(1.303);

            var location = KeplerianHelper.GetLocation(centricDistance, ascendingNodeLongitude, perihelionOmega, trueAnomaly, inclination);

            var x = location.X;
            var y = location.Y;
            var z = location.Z;

            Assert.True(CompareUtilities.EqualsWithinTolerance(x, -5.04289, 4));
            Assert.True(CompareUtilities.EqualsWithinTolerance(y, 1.93965, 4));
            Assert.True(CompareUtilities.EqualsWithinTolerance(z, 0.10478, 4));
        }
    }
}