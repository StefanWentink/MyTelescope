namespace MyTelescope.Utilities.Models
{
    using Helpers;

    public class DegreeModel
    {
        public DegreeModel(double value)
        {
            Degrees = value;
        }

        public double Degrees { get; private set; }

        public double Radians
        {
            get => DegreeHelper.DegreesToRadians(Degrees);
            private set => Degrees = DegreeHelper.RadiansToDegrees(value);
        }

        public void SetDegrees(double value)
        {
            Degrees = value;
        }

        public void SetRadians(double value)
        {
            Radians = value;
        }
    }
}