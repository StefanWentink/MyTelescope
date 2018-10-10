namespace MyTelescope.App.Localisation.Models
{
    using System.Globalization;
    using System.Threading;
    using Interfaces;
    using Utilities.Helpers;

    public abstract class LocaliseBase : ILocalise
    {
        protected const string DefaultLanguageCode = "en";

        public void SetLocale(CultureInfo cultureInfo)
        {
            Thread.CurrentThread.CurrentCulture = cultureInfo;
            Thread.CurrentThread.CurrentUICulture = cultureInfo;
        }

        protected static (string languageCode, CultureInfo cultureInfo) CachedCulture { get; set; }

        public CultureInfo GetCurrentCultureInfo()
        {
            var languageCode = PlatformLanguageCode();

            if (!string.IsNullOrEmpty(CachedCulture.languageCode) && CachedCulture.languageCode.Equals(languageCode))
            {
                return CachedCulture.cultureInfo;
            }

            // this gets called a lot - try/catch can be expensive so consider caching or something
            CultureInfo cultureInfo;
            try
            {
                cultureInfo = new CultureInfo(languageCode);
            }
            catch (CultureNotFoundException primaryException)
            {
                LogHelper.LogError(primaryException);

                // iOS locale not valid .NET culture (eg. "en-ES" : English in Spain)
                // fallback to first characters, in this case "en"
                try
                {
                    languageCode = ToDotnetFallbackLanguage(new PlatformCultureInfo(languageCode));
                    cultureInfo = new CultureInfo(languageCode);
                }
                catch (CultureNotFoundException secondaryException)
                {
                    LogHelper.LogError(secondaryException);

                    // iOS language not valid .NET culture, falling back to English
                    languageCode = DefaultLanguageCode;
                    cultureInfo = new CultureInfo(languageCode);
                }
            }

            CachedCulture = (languageCode, cultureInfo);

            return CachedCulture.cultureInfo;
        }

        protected abstract string PlatformLanguageCode();

        protected abstract string ToDotnetFallbackLanguage(PlatformCultureInfo platformCultureInfo);
    }
}
