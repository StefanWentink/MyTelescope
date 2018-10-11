namespace MyTelescope.Utilities.Helpers
{
    public static class BoolHelper
    {
        public static bool BoolIsNullOrEmpty(object value)
        {
            return TypeHelper.ToType(value, TryParseBool, default(bool)) == default(bool);
        }

        public static bool ToBool(object value)
        {
            return TypeHelper.ToType(value, TryParseBool, default(bool));
        }

        public static bool? ToBoolOrNull(object value)
        {
            return TypeHelper.ToTypeOrNull<bool>(value, TryParseNullableBool, null);
        }

        private static bool TryParseBool(object value, bool defaultValue)
        {
            return bool.TryParse(value.ToString(), out var result)
                ? result
                : defaultValue;
        }

        private static bool? TryParseNullableBool(object value, bool? defaultValue)
        {
            return bool.TryParse(value.ToString(), out var result)
                ? result
                : defaultValue;
        }
    }
}