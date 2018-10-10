using Xamarin.Forms;

[assembly: Dependency(typeof(MyTelescope.App.iOS.Components.Localise))]

namespace MyTelescope.App.iOS.Components
{
    using Foundation;
    using MyTelescope.App.Localisation.Models;

    public class Localise : LocaliseBase
    {
        protected override string PlatformLanguageCode()
        {
            if (NSLocale.PreferredLanguages.Length <= 0)
            {
                return DefaultLanguageCode;
            }

            var preferredLanguages = NSLocale.PreferredLanguages[0];
            return PlatformToDotnetLanguage(preferredLanguages);
        }

        protected override string ToDotnetFallbackLanguage(PlatformCultureInfo platformCultureInfo)
        {
            var languageCode = platformCultureInfo.LanguageCode; // use the first part of the identifier (two chars, usually);

            switch (platformCultureInfo.LanguageCode)
            {
                case "pt":
                    languageCode = "pt-PT"; // fallback to Portuguese (Portugal)
                    break;
                case "gsw":
                    languageCode = "de-CH"; // equivalent to German (Switzerland) for this app
                    break;

                    // add more application-specific cases here (if required)
                    // ONLY use cultures that have been tested and known to work
            }

            return languageCode;
        }

        private static string PlatformToDotnetLanguage(string platformLanguage)
        {
            var netLanguage = platformLanguage;

            // certain languages need to be converted to CultureInfo equivalent
            switch (platformLanguage)
            {
                case "ms-MY":   // "Malaysian (Malaysia)" not supported .NET culture
                case "ms-SG":   // "Malaysian (Singapore)" not supported .NET culture
                    netLanguage = "ms"; // closest supported
                    break;
                case "gsw-CH":  // "Schwiizertüütsch (Swiss German)" not supported .NET culture
                    netLanguage = "de-CH"; // closest supported
                    break;

                    // add more application-specific cases here (if required)
                    // ONLY use cultures that have been tested and known to work
            }

            return netLanguage;
        }
    }
}