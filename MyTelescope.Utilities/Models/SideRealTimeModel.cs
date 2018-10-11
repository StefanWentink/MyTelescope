namespace MyTelescope.Utilities.Models
{
    using System;

    public class SideRealTimeModel
    {
        public SideRealTimeModel(DateTimeOffset dateTimeOffset, double sideRealTime)
        {
            DateTimeOffset = dateTimeOffset;
            SideRealTime = sideRealTime;
        }

        public DateTimeOffset DateTimeOffset { get; }

        public double SideRealTime { get; }
    }
}