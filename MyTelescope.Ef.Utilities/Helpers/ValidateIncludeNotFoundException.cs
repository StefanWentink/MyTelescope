namespace MyTelescope.Ef.Utilities.Helpers
{
    using System;
    using Core.Utilities.Helpers;
    using MyTelescope.Utilities.Helpers;

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
