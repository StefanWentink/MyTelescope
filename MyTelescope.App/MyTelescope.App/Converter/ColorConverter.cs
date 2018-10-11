namespace MyTelescope.App.Converter
{
    using System;
    using System.Globalization;
    using Utilities.Helpers;
    using Xamarin.Forms;

    internal class ColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ColorConvertHelper.Convert(value as string);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ColorConvertHelper.ConvertBack();
        }
    }
}