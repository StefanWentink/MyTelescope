namespace MyTelescope.Test.Data
{
    using System;

    internal class DataModel
    {
        public string DataString { get; set; }

        public int DataInt { get; set; }

        public int? DataNullableInt { get; set; }

        public double DataDouble { get; set; }

        public double? DataNullableDouble { get; set; }

        public DateTimeOffset DataDateTimeOffset { get; set; }

        public DateTimeOffset? DataNullableDateTimeOffset { get; set; }

        public bool DataBool { get; set; }

        public bool? DataNullableBool { get; set; }

        public Guid DataGuid { get; set; }

        public Guid? DataNullableGuid { get; set; }
    }
}
