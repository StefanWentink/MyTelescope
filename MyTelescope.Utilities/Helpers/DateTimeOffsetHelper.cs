namespace MyTelescope.Utilities.Helpers
{
    using System;

    public static class DateTimeOffsetHelper
    {
        public static bool DateTimeOffsetIsNullOrEmpty(object value)
        {
            return TypeHelper.ToType(value, TryParseDateTimeOffset, default(DateTimeOffset)) == default(DateTimeOffset);
        }

        public static DateTimeOffset ToDateTimeOffset(object value)
        {
            return TypeHelper.ToType(value, TryParseDateTimeOffset, default(DateTimeOffset));
        }

        public static DateTimeOffset? ToDateTimeOffsetOrNull(object value)
        {
            return TypeHelper.ToTypeOrNull<DateTimeOffset>(value, TryParseNullableDateTimeOffset, null);
        }

        private static DateTimeOffset TryParseDateTimeOffset(object value, DateTimeOffset defaultValue)
        {
            return DateTimeOffset.TryParse(value.ToString(), out var result)
                ? result
                : defaultValue;
        }

        private static DateTimeOffset? TryParseNullableDateTimeOffset(object value, DateTimeOffset? defaultValue)
        {
            return DateTimeOffset.TryParse(value.ToString(), out var result)
                ? result
                : defaultValue;
        }

        public static DateTimeOffset GetClosestUtcZeroTime(this DateTime date)
        {
            var localDate = new DateTimeOffset(date);
            var utcDate = new DateTimeOffset(date).ToUniversalTime();

            return utcDate + localDate.Offset;
        }
    }
}
