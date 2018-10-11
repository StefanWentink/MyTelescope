namespace MyTelescope.Ef.Utilities.Helpers
{
    using Core.Utilities.Helpers;
    using System;

    public static class IncludePathExceptionHelper
    {
        public static void ValidateIncludeNotFoundException<TModel>(this InvalidOperationException ex)
        {
            var message = ex.Message;

            if (message.StartsWith("A specified Include path is not valid."))
            {
                LogHelper.LogError(string.Format("Include error on ${0}.", typeof(TModel)));
            }
        }
    }
}