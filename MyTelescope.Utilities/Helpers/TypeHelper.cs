namespace MyTelescope.Utilities.Helpers
{
    using System;

    internal static class TypeHelper
    {
        internal static T? ToTypeOrNull<T>(object value, Func<object, T?, T?> function, T? defaultValue)
            where T : struct
        {
            if (value is T filterValue)
            {
                return filterValue;
            }

            if (value == null)
            {
                return defaultValue;
            }

            return function(value, defaultValue);
        }

        internal static T ToType<T>(object value, Func<object, T, T> function, T defaultValue)
            where T : struct
        {
            if (value is T filterValue)
            {
                return filterValue;
            }

            if (value == null)
            {
                return defaultValue;
            }

            return function(value, defaultValue);
        }
    }
}