namespace MyTelescope.App.Utilities.Models
{
    using MyTelescope.Utilities.Models;
    using System;
    using System.Drawing;

    public class CelestialDrawModel : BaseDrawModel<double>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CelestialDrawModel"/> class.
        /// For use of compare
        /// </summary>
        /// <param name="id"></param>
        /// <param name="description"></param>
        /// <param name="radius"></param>
        /// <param name="color"></param>
        public CelestialDrawModel(Guid id, string description, double radius, Color color)
            : base(id, description, radius, null, color, null, null, 0, 100)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CelestialDrawModel"/> class.
        /// for use of orbit
        /// </summary>
        /// <param name="id"></param>
        /// <param name="description"></param>
        /// <param name="radius"></param>
        /// <param name="location"></param>
        /// <param name="borderColor"></param>
        /// <param name="strokeWidth"></param>
        /// <param name="opacity"></param>
        public CelestialDrawModel(Guid id, string description, double radius, LocationModel location, Color borderColor, int strokeWidth, int opacity)
            : base(id, description, radius, location, null, borderColor, null, strokeWidth, opacity)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CelestialDrawModel"/> class.
        /// for use of celestialbody
        /// </summary>
        /// <param name="id"></param>
        /// <param name="description"></param>
        /// <param name="radius"></param>
        /// <param name="location"></param>
        /// <param name="opacity"></param>
        /// <param name="color"></param>
        public CelestialDrawModel(Guid id, string description, double radius, LocationModel location, Color color, int opacity)
            : base(id, description, radius, location, color, null, null, 0, opacity)
        {
        }
    }
}