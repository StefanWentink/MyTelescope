namespace MyTelescope.Utilities.Helpers
{
    using System;

    public static class ExceptionHelper
    {
        public static Exception GetInnerMostException(this Exception exception)
        {
            var result = exception;

            while (result?.InnerException != null)
            {
                result = result.InnerException;
            }

            return result;
        }
    }
}