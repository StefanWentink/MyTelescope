namespace MyTelescope.Test.Data
{
    using System;

    internal class RefelectionNullablePropertyModel
    {
        internal string NullableString { get; set; }

        internal int NonNullableInt { get; set; }

        internal int? NullableInt { get; set; }

        internal Guid NonNullableGuid { get; set; }

        internal Guid? NullableGuid { get; set; }

        internal DateTimeOffset NonNullableDateTimeOffset { get; set; }

        internal DateTimeOffset? NullableDateTimeOffset { get; set; }
    }
}