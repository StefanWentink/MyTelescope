namespace MyTelescope.Utilities.Helpers
{
    using System;

    public static class GuidHelper
    {
        public static bool GuidIsNullOrEmpty(object value)
        {
            return TypeHelper.ToType(value, TryParseGuid, default(Guid)) == default(Guid);
        }

        public static Guid ToGuid(object value)
        {
            return TypeHelper.ToType(value, TryParseGuid, default(Guid));
        }

        public static Guid? ToGuidOrNull(object value)
        {
            return TypeHelper.ToTypeOrNull<Guid>(value, TryParseNullableGuid, null);
        }

        private static Guid TryParseGuid(object value, Guid defaultValue)
        {
            return Guid.TryParse(value.ToString(), out var result)
                ? result
                : defaultValue;
        }

        private static Guid? TryParseNullableGuid(object value, Guid? defaultValue)
        {
            return Guid.TryParse(value.ToString(), out var result)
                ? result
                : defaultValue;
        }
    }
}