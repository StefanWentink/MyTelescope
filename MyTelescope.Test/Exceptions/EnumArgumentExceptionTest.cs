namespace MyTelescope.Test.Exceptions
{
    using Base;
    using Data;
    using MyTelescope.Utilities.Exceptions;
    using System;
    using Xunit;

    public class EnumArgumentExceptionTest : IClassFixture<CustomFixture>
    {
        [Fact]
        public void EnumArgumentExceptionConstructorTest()
        {
            var exception = new EnumArgumentException<TestEnumerationType>(3);
            Assert.Contains("is not a valid value for", exception.Message, StringComparison.OrdinalIgnoreCase);
        }
    }
}