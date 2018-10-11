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
            switch (platformCultureInfo.LanguageCode)
            {
                case "pt":
                    return "pt-PT"; // fallback to Portuguese (Portugal)

                case "gsw":
                    return "de-CH"; // equivalent to German (Switzerland) for this app

                    // add more application-specific cases here (if required)
                    // ONLY use cultures that have been tested and known to work
            }

            return platformCultureInfo.LanguageCode;
        }

        private static string PlatformToDotnetLanguage(string platformLanguage)
        {
            // certain languages need to be converted to CultureInfo equivalent
            switch (platformLanguage)
            {
                case "ms-MY":   // "Malaysian (Malaysia)" not supported .NET culture
                case "ms-SG":   // "Malaysian (Singapore)" not supported .NET culture
                    return "ms"; // closest supported

                case "gsw-CH":  // "Schwiizertüütsch (Swiss German)" not supported .NET culture
                    return "de-CH"; // closest supported

                    // add more application-specific cases here (if required)
                    // ONLY use cultures that have been tested and known to work
            }

            return platformLanguage;
        }
    }
}