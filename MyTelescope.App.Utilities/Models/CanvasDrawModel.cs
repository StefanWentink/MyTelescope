namespace MyTelescope.App.Utilities.Models
{
    using System;
    using System.Drawing;
    using Interfaces;
    using MyTelescope.Utilities.Models;

    public class CanvasDrawModel : BaseDrawModel<int>, ICanvasDrawModel
    {
        public int CentreX { get; set; }

        public int CentreY { get; set; }

        public CanvasDrawModel(
            Guid id,
            string description,
            int radius,
            LocationModel location,
            Color? color,
            Color? borderColor,
            Color? secondaryColor,
            int strokeWidth)
            : this(
                id,
                description,
                radius,
                (int)location.X,
                (int)location.Y,
                color,
                borderColor,
                secondaryColor,
                strokeWidth)
        {
        }

        public CanvasDrawModel(
            Guid id,
            string description,
            int radius,
            int centreX,
            int centreY,
            Color? color,
            Color? borderColor,
            Color? secondaryColor,
            int strokeWidth)
                : base(
                    id,
                    description,
                    radius,
                    null,
                    color,
                    borderColor,
                    secondaryColor,
                    strokeWidth, 
                    50)
        {
            CentreX = centreX;
            CentreY = centreY;
        }
    }
}
