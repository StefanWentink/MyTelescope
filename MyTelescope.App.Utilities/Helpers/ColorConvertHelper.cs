namespace MyTelescope.App.Utilities.Helpers
{
    using System;
    using System.Drawing;

    public static class ColorConvertHelper
    {
        public static Color Convert(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return Color.Black;
            }

            if (value.ToLowerInvariant().Contains("who"))
            {
                return Color.Red;
            }

            return Color.Black;
        }

        public static Color ConvertBack()
        {
            throw new NotImplementedException($"{nameof(ColorConvertHelper)}.{nameof(ConvertBack)} is not implemented.");
        }
    }
}
