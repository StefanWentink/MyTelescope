namespace MyTelescope.App.Helpers.Di
{
    using Localisation.Interfaces;
    using Localisation.Resources.MyTelescope;
    using Xamarin.Forms;

    internal static class ResourceDiHelper
    {
        internal static void ConfigureServices()
        {
            if (Device.RuntimePlatform == Device.iOS || Device.RuntimePlatform == Device.Android)
            {
                var cultureInfo = DependencyService.Get<ILocalise>().GetCurrentCultureInfo();
                TextResource.Culture = cultureInfo; // set the RESX for resource localization
                DependencyService.Get<ILocalise>().SetLocale(cultureInfo); // set the Thread for locale-aware methods
            }
        }
    }
}