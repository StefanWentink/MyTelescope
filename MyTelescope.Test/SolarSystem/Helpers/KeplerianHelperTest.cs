namespace MyTelescope.Test.SolarSystem.Helpers
{
    using System;
    using Base;
    using MyTelescope.SolarSystem.Helpers;
    using MyTelescope.Utilities.Helpers;
    using MyTelescope.Utilities.Models;
    using Xunit;

    public class KeplerianHelperTest : IClassFixture<CustomFixture>
    {
        [Fact]
        public void KeplerianHelperGetJ2000CenturyFactorTest()
        {
            var value = KeplerianHelper.GetJ2000CenturyFactor(new DateTimeOffset(1900, 1, 1, 0, 0, 0, TimeSpan.Zero));
            Assert.True(value.EqualsWithinTolerance(-1, 5));

            value = KeplerianHelper.GetJ2000CenturyFactor(new DateTimeOffset(2000, 1, 1, 0, 0, 0, TimeSpan.Zero));
            Assert.Equal(0, value);

            value = KeplerianHelper.GetJ2000CenturyFactor(new DateTimeOffset(2100, 1, 1, 0, 0, 0, TimeSpan.Zero));
            Assert.True(value.EqualsWithinTolerance(1, 4));
        }

        [Fact]
        public void KeplerianHelperTEst()
        {
            var centricDistance = 5.40406;
            var ascendingNodeLongitude = new DegreeModel(100.464);
            var perihelionOmega = new DegreeModel(273.867);
            var trueAnomaly = new DegreeModel(144.637);
            var inclination = new DegreeModel(1.303);

            var location = KeplerianHelper.GetLocation(centricDistance, ascendingNodeLongitude, perihelionOmega, trueAnomaly, inclination);

            var x = location.X;
            var y = location.Y;
            var z = location.Z;

            Assert.True(x.EqualsWithinTolerance(-5.04289, 4));
            Assert.True(y.EqualsWithinTolerance(1.93965, 4));
            Assert.True(z.EqualsWithinTolerance(0.10478, 4));
        }
    }
}
