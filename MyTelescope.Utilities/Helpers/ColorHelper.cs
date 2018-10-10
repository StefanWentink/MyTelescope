namespace MyTelescope.Utilities.Helpers
{
    using System;
    using System.Drawing;
    using System.Globalization;

    public static class ColorHelper
    {
        public static Color GetColorFromHex(string hexColor)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(hexColor, @"[#]([0-9]|[a-f]|[A-F]){6}\b"))
            {
                throw new ArgumentException($"{hexColor} is not in the correct format.", nameof(hexColor));
            }

            var red = int.Parse(hexColor.Substring(1, 2), NumberStyles.HexNumber);
            var green = int.Parse(hexColor.Substring(3, 2), NumberStyles.HexNumber);
            var blue = int.Parse(hexColor.Substring(5, 2), NumberStyles.HexNumber);

            return Color.FromArgb(red, green, blue);
        }

        public static Color? SetOpacity(this Color? color, int opacity)
        {
            if (!color.HasValue)
            {
                return null;
            }

            return color.Value.SetOpacity(opacity);
        }

        public static Color SetOpacity(this Color color, int opacity)
        {
            if (opacity == 100)
            {
                return color;
            }

            var normalizedPercentage = opacity;

            if (opacity > 100)
            {
                normalizedPercentage = 100;
            }

            if (opacity < 0)
            {
                normalizedPercentage = 0;
            }

            var hexValue = (int)Math.Round(normalizedPercentage * 2.56);

            return Color.FromArgb(hexValue, color.R, color.G, color.B);
        }
    }
}
