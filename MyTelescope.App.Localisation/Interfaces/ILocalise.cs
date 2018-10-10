namespace MyTelescope.App.Localisation.Interfaces
{
    using System.Globalization;

    /// <summary>
    /// Interface support Localisation of otherwise static text
    /// </summary>
    public interface ILocalise
    {
        /// <summary>
        /// Get <see cref="CultureInfo"/>
        /// </summary>
        /// <returns></returns>
        CultureInfo GetCurrentCultureInfo();

        /// <summary>
        /// Set <see cref="CultureInfo"/>
        /// </summary>
        /// <param name="cultureInfo">
        /// The culture Info.
        /// </param>
        void SetLocale(CultureInfo cultureInfo);
    }
}
