namespace MyTelescope.Utilities.Models
{
    using System;

    public class RiseTransitSetModel
    {
        public RiseTransitSetModel(DateTimeOffset rise, DateTimeOffset transit, DateTimeOffset set)
        {
            Rise = rise;
            Transit = transit;
            Set = set;
        }

        public DateTimeOffset Rise { get; }

        public DateTimeOffset Transit { get; }

        public DateTimeOffset Set { get; }
    }
}