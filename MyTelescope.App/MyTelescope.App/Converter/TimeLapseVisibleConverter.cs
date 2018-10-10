namespace MyTelescope.App.Converter
{
    using System;
    using System.Globalization;
    using Utilities.Helpers;
    using Xamarin.Forms;

    internal class TimeLapseVisibleConverter : IValueConverter
    {
        public bool ConvertSelectedIndex(int value)
        {
            if (Convert(value, typeof(bool), null, CultureInfo.CurrentCulture) is bool result)
            {
                return result;
            }

            return false;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var stringValue = value == null
                ? string.Empty
                : value.ToString();
            return BoolConvertHelper.ConvertCompare(stringValue, "0");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return BoolConvertHelper.ConvertBack();
        }
    }
}
