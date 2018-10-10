using Xamarin.Forms;

[assembly: Dependency(typeof(MyTelescope.App.UWP.Components.Localise))]

namespace MyTelescope.App.UWP.Components
{
    using Localisation.Interfaces;
    using System.Globalization;

    public class Localise : ILocalise
    {
        public void SetLocale(CultureInfo cultureInfo)
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture = cultureInfo;
        }

        public CultureInfo GetCurrentCultureInfo()
        {
            return System.Threading.Thread.CurrentThread.CurrentUICulture;
        }
    }
}