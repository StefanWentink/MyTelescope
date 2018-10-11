namespace MyTelescope.Utilities.Models
{
    public class LocationModel
    {
        public LocationModel(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        /// <summary>
        /// X -x
        /// </summary>
        public double X { get; }

        /// <summary>
        /// Y - y
        /// </summary>
        public double Y { get; }

        /// <summary>
        /// Z - z
        /// </summary>
        public double Z { get; }
    }
}