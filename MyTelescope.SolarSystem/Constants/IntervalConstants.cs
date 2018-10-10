namespace MyTelescope.SolarSystem.Constants
{
    using System.Collections.Generic;
    using Enums;
    using Utilities.Helpers;

    public static class IntervalConstants
    {
        private static Dictionary<Interval, double> _intervalDegreeFactors;

        private static Dictionary<Interval, double> IntervalDegreeFactor => 
            _intervalDegreeFactors ?? (_intervalDegreeFactors = GetIntervalDegreeFactors());

        public static double GetDegreefactor(this Interval interval)
        {
            return IntervalDegreeFactor[interval];
        }

        private static Dictionary<Interval, double> GetIntervalDegreeFactors()
        {
            var result = new Dictionary<Interval, double>();

            foreach (var interval in EnumHelper.GetValues<Interval>())
            {
                result.Add(interval, (double) interval * 0.25);
            }

            return result;
        }
    }
}