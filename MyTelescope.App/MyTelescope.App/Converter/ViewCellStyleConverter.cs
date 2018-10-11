namespace MyTelescope.App.Converter
{
    using System;
    using System.Globalization;
    using Utilities.Helpers;
    using Xamarin.Forms;

    internal class ViewCellStyleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var stringValue = value == null
                ? string.Empty
                : value.ToString();
            return BoolConvertHelper.ConvertCompare(stringValue, "true") ? "ViewCellSelected" : "ViewCellBasic";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return BoolConvertHelper.ConvertBack();
        }
    }
}