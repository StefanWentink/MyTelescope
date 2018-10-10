namespace MyTelescope.Utilities.Helpers
{
    using System;

    public static class ToleranceHelper
    {
        public static bool EqualsWithinTolerance(this double value, double compare, double tolerance)
        {
            var difference = Math.Abs(value - compare);
            var compareTolerance = 1 / Math.Pow(10, tolerance);

            return difference < compareTolerance;
        }
    }
}
