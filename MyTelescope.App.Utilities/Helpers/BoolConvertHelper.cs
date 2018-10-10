namespace MyTelescope.App.Utilities.Helpers
{
    using System;

    public static class BoolConvertHelper
    {
        public static bool ConvertIsNullOrWhiteSpace(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return true;
            }

            return false;
        }

        public static bool ConvertCompare(string value, string compare)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return false;
            }

            return value.Equals(compare, StringComparison.OrdinalIgnoreCase);
        }

        public static bool ConvertBack()
        {
            throw new NotImplementedException($"{nameof(BoolConvertHelper)}.{nameof(ConvertBack)} is not implemented.");
        }
    }
}
