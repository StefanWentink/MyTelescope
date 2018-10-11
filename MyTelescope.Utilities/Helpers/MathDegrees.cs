namespace MyTelescope.Utilities.Helpers
{
    public static class MathDegrees
    {
        public static double Cos(double value)
        {
            var radians = DegreeHelper.DegreesToRadians(value);
            var result = System.Math.Cos(radians);
            return result;
        }

        public static double Sin(double value)
        {
            var radians = DegreeHelper.DegreesToRadians(value);
            var result = System.Math.Sin(radians);
            return result;
        }

        public static double Tan(double value)
        {
            var radians = DegreeHelper.DegreesToRadians(value);
            var result = System.Math.Tan(radians);
            return result;
        }

        public static double Acos(double value)
        {
            var radians = DegreeHelper.DegreesToRadians(value);
            var result = System.Math.Acos(radians);
            return result;
        }

        public static double Atan(double value)
        {
            var radians = DegreeHelper.DegreesToRadians(value);
            var result = System.Math.Atan(radians);
            return result;
        }

        public static double Asin(double value)
        {
            var radians = DegreeHelper.DegreesToRadians(value);
            var result = System.Math.Asin(radians);
            return result;
        }

        public static double Atan2(double value, double secondaryValue)
        {
            var radians = DegreeHelper.DegreesToRadians(value);
            var secondaryRadians = DegreeHelper.DegreesToRadians(secondaryValue);
            var result = System.Math.Atan2(radians, secondaryRadians);
            return result;
        }
    }
}