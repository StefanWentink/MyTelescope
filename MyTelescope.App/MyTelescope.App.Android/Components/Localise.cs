using Xamarin.Forms;

[assembly: Dependency(typeof(MyTelescope.App.Droid.Components.Localise))]

namespace MyTelescope.App.Droid.Components
{
    using Localisation.Models;

    using MyTelescope.Utilities.Helpers;

    public class Localise : LocaliseBase
    {
        protected override string PlatformLanguageCode()
        {
            var localeCode = Java.Util.Locale.Default.ToString();

            return PlatformToDotnetLanguage(CultureInfoHelper.CleanCutureCode(localeCode));
        }

        protected override string ToDotnetFallbackLanguage(PlatformCultureInfo platformCultureInfo)
        {
            switch (platformCultureInfo.LanguageCode)
            {
                case "gsw":
                    return "de-CH";
            }

            return platformCultureInfo.LanguageCode;
        }

        private static string PlatformToDotnetLanguage(string platformLanguage)
        {
            var languageCode = platformLanguage;

            switch (languageCode)
            {
                case "ms-BN":   // "Malaysian (Brunei)" not supported .NET culture
                case "ms-MY":   // "Malaysian (Malaysia)" not supported .NET culture
                case "ms-SG":   // "Malaysian (Singapore)" not supported .NET culture
                    languageCode = "ms"; // closest supported
                    break;

                case "in-ID":  // "Indonesian (Indonesia)" has different code in  .NET
                    languageCode = "id-ID"; // correct code for .NET
                    break;

                case "gsw-CH":  // "Schwiizertüütsch (Swiss German)" not supported .NET culture
                    languageCode = "de-CH"; // closest supported
                    break;

                default:
                    return languageCode;

                    // add more application-specific cases here (if required)
                    // ONLY use cultures that have been tested and known to work
            }

            return languageCode;
        }
    }
}