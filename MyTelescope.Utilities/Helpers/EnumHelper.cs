namespace MyTelescope.Utilities.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class EnumHelper
    {
        public static IEnumerable<TEnum> GetValues<TEnum>(Func<TEnum, bool> expression)
            where TEnum : struct, IConvertible
        {
            return GetValues<TEnum>().Where(expression);
        }

        public static IEnumerable<TEnum> GetValues<TEnum>()
            where TEnum : struct, IConvertible
        {
            return Enum.GetValues(typeof(TEnum)).Cast<TEnum>();
        }
    }
}
