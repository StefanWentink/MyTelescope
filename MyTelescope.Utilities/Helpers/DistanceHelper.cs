namespace MyTelescope.Utilities.Helpers
{
    using Constants;

    public static class DistanceHelper
    {
        public static double AstronomicalUnitsToKilometers(double value)
        {
            return value * DistanceConstants.AstronomicalUnitInKm;
        }

        public static double KilometersToAstronomicalUnits(double value)
        {
            return value / DistanceConstants.AstronomicalUnitInKm;
        }

        /// <summary>
        /// Astronomical units to 10 pow 6 kilometers
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static double AstronomicalUnitsToMillionKilometers(double value)
        {
            return value * (DistanceConstants.AstronomicalUnitInKm / 1000000);
        }

        /// <summary>
        /// 10 pow 6 kilometers to astronomical units
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static double MillionKilometersToAstronomicalUnits(double value)
        {
            return value / (DistanceConstants.AstronomicalUnitInKm / 1000000);
        }
    }
}