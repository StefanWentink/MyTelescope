namespace MyTelescope.App.Converter
{
    using Helpers;
    using System;
    using System.Globalization;
    using Xamarin.Forms;

    internal class ImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ImageConvertHelper.Convert(value as string);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ImageConvertHelper.ConvertBack(value);
        }
    }
}