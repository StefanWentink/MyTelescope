namespace MyTelescope.Utilities.Extensions
{
    using Helpers;
    using Models;

    public static class DegreeModelExtensions
    {
        public static DegreeModel Modulo360Absolute(this DegreeModel model)
        {
            return new DegreeModel(DegreeHelper.Modulo360Absolute(model.Degrees));
        }

        public static DegreeModel Modulo360AroundZero(this DegreeModel model)
        {
            return new DegreeModel(DegreeHelper.Modulo360AroundZero(model.Degrees));
        }

        public static double Cos(this DegreeModel model)
        {
            return MathDegrees.Cos(model.Degrees);
        }

        public static double Sin(this DegreeModel model)
        {
            return MathDegrees.Sin(model.Degrees);
        }
    }
}
