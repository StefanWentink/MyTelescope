namespace MyTelescope.App.Converter
{
    using System;
    using System.Globalization;
    using Utilities.Helpers;
    using Xamarin.Forms;

    internal class BoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !BoolConvertHelper.ConvertIsNullOrWhiteSpace(value as string);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return BoolConvertHelper.ConvertBack();
        }
    }
}