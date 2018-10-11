namespace MyTelescope.Utilities.Models
{
    using System;

    public class SkyPositionModel
    {
        public SkyPositionModel(DateTimeOffset dateTimeOffset, double heigth, double azimuth)
        {
            DateTimeOffset = dateTimeOffset;
            Heigth = heigth;
            Azimuth = azimuth;
        }

        public DateTimeOffset DateTimeOffset { get; }

        public double Heigth { get; }

        public double Azimuth { get; }
    }
}