namespace MyTelescope.Utilities.Exceptions
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class EnumArgumentException<TEnum> : ArgumentException
        where TEnum : struct, IConvertible
    {
        [Obsolete("Only for serialisation")]
        public EnumArgumentException()
        {
        }

        public EnumArgumentException(int value)
            : base(!typeof(TEnum).IsEnum
                 ? $"{typeof(TEnum)} is not of basetype enum {nameof(Enum)}."
                 : $"{value} is not a valid value for {typeof(TEnum).Name}.")
        {
        }

        [Obsolete("Only for serialization")]
        protected EnumArgumentException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
