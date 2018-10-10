namespace MyTelescope.App.Localisation.Models
{
    using System;
    using MyTelescope.Utilities.Helpers;

    public class PlatformCultureInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlatformCultureInfo"/> class. 
        /// Container for culture info parts
        /// </summary>
        /// <param name="cultureCode">
        /// </param>
        /// <exception cref="ArgumentException">
        /// If 
        /// <exception cref="cultureCode">
        /// is null or string.Empty
        /// </exception>
        /// </exception>
        public PlatformCultureInfo(string cultureCode)
        {
            if (string.IsNullOrEmpty(cultureCode))
            {
                throw new ArgumentException("Expected culture identifier", nameof(cultureCode));
            }

            var (languageCode, localeCode) = CultureInfoHelper.SplitCultureCodeParts(cultureCode);

            LanguageCode = languageCode;
            LocaleCode = localeCode;
        }

        public string LanguageCode { get; }

        public string LocaleCode { get; }

        public override string ToString()
        {
            return CultureInfoHelper.ConcatCultureCodeParts(LanguageCode, LocaleCode);
        }
    }
}
