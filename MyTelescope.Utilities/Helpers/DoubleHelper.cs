namespace MyTelescope.Utilities.Helpers
{
    public static class DoubleHelper
    {
        public static bool DoubleIsNullOrEmpty(object value)
        {
            return TypeHelper.ToType(value, TryParseDouble, default(double)).EqualsWithinTolerance(default(double), 6);
        }

        public static double ToDouble(object value)
        {
            return TypeHelper.ToType(value, TryParseDouble, default(double));
        }

        public static double? ToDoubleOrNull(object value)
        {
            return TypeHelper.ToTypeOrNull<double>(value, TryParseNullableDouble, null);
        }

        private static double TryParseDouble(object value, double defaultValue)
        {
            return double.TryParse(value.ToString(), out var result)
                ? result
                : defaultValue;
        }

        private static double? TryParseNullableDouble(object value, double? defaultValue)
        {
            return double.TryParse(value.ToString(), out var result)
                ? result
                : defaultValue;
        }

        public static double GetMax(double value, double compare)
        {
            return value > compare ? value : compare;
        }

        public static double GetMin(double value, double compare)
        {
            return value < compare ? value : compare;
        }
    }
}
