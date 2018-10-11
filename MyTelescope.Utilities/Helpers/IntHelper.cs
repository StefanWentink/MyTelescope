namespace MyTelescope.Utilities.Helpers
{
    public static class IntHelper
    {
        public static bool IntIsNullOrEmpty(object value)
        {
            return TypeHelper.ToType(value, TryParseInt, default(int)) == default(int);
        }

        public static int ToInt(object value)
        {
            return TypeHelper.ToType(value, TryParseInt, default(int));
        }

        public static int? ToIntOrNull(object value)
        {
            return TypeHelper.ToTypeOrNull<int>(value, TryParseNullableInt, null);
        }

        private static int TryParseInt(object value, int defaultValue)
        {
            return int.TryParse(value.ToString(), out var result)
                ? result
                : defaultValue;
        }

        private static int? TryParseNullableInt(object value, int? defaultValue)
        {
            return int.TryParse(value.ToString(), out var result)
                ? result
                : defaultValue;
        }

        public static int GetMax(int value, int compare)
        {
            return value > compare ? value : compare;
        }

        public static int GetMin(int value, int compare)
        {
            return value < compare ? value : compare;
        }
    }
}