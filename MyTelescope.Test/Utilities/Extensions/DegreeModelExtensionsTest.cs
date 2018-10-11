namespace MyTelescope.Test.Utilities.Extensions
{
    using Base;
    using MyTelescope.Utilities.Extensions;
    using MyTelescope.Utilities.Helpers;
    using MyTelescope.Utilities.Models;
    using SWE.BasicType.Utilities;
    using Xunit;

    public class DegreeModelExtensionsTest : IClassFixture<CustomFixture>
    {
        [Theory]
        [InlineData(3420, 180)]
        [InlineData(360.1, 0.1)]
        [InlineData(360, 0)]
        [InlineData(359.9, 359.9)]
        [InlineData(0, 0)]
        [InlineData(-0.1, 359.9)]
        public void Modulo360AbsoluteTest(double value, double expected)
        {
            var model = new DegreeModel(value);
            var actual = model.Modulo360Absolute();
            Assert.True(CompareUtilities.EqualsWithinTolerance(expected, actual.Degrees, 6), $"expected {expected} and actual {actual.Degrees} are not equal.");
        }

        [Theory]
        [InlineData(3420, 0)]
        [InlineData(360.1, -179.9)]
        [InlineData(360, -180)]
        [InlineData(359.9, 179.9)]
        [InlineData(0, -180)]
        [InlineData(-0.1, 179.9)]
        public void Modulo360AroundZeroTest(double value, double expected)
        {
            var model = new DegreeModel(value);
            var actual = model.Modulo360AroundZero();
            Assert.True(CompareUtilities.EqualsWithinTolerance(expected, actual.Degrees, 6), $"expected {expected} and actual {actual.Degrees} are not equal.");
        }
    }
}