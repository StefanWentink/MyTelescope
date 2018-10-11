namespace MyTelescope.Utilities.Helpers
{
    using System;

    public static class DegreeHelper
    {
        public static double RadiansToDegrees(double value)
        {
            return (180 / Math.PI) * value;
        }

        public static double DegreesToRadians(double value)
        {
            return (Math.PI / 180.0) * value;
        }

        public static double Modulo360Absolute(double value)
        {
            var result = value;

            while (result < 360)
            {
                result += 360;
            }

            return result % 360;
        }

        public static double Modulo360AroundZero(double value)
        {
            return Modulo360Absolute(value) - 180;
        }

        private const double DegreeMinuteFactor = 4;

        private const double DegreeMillisecondFactor = DegreeMinuteFactor * 60000;

        public static double TimeSpanToDegree(TimeSpan timespan)
        {
            return timespan.TotalMilliseconds / DegreeMillisecondFactor;
        }

        public static TimeSpan DegreeToTimeSpan(double degree)
        {
            return TimeSpan.FromMilliseconds(degree * DegreeMillisecondFactor);
        }
    }
}