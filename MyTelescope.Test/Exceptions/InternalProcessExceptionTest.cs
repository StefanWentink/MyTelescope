namespace MyTelescope.Test
{
    using Base;
    using MyTelescope.Utilities.Exceptions;
    using MyTelescope.Utilities.Helpers;
    using Xunit;

    public class InternalProcessExceptionTest : IClassFixture<CustomFixture>
    {
        [Fact]
        public void InternalProcessExceptionConstructorTest()
        {
            var exception = new InternalProcessException("test1");
            exception = new InternalProcessException(exception);
            exception = new InternalProcessException("test2", exception);

            Assert.Equal("test2", exception.Message);
            Assert.Equal("test1", exception.GetInnerMostException().Message);
        }
    }
}