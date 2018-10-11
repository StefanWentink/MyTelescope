namespace MyTelescope.App.Utilities.Models
{
    using MyTelescope.Utilities.Helpers;
    using MyTelescope.Utilities.Models;
    using System;
    using System.Drawing;

    public abstract class BaseDrawModel<TValue>
    {
        public TValue Radius { get; }

        public Guid Id { get; }

        public string Description { get; }

        public Color? Color { get; }

        public Color? SecondaryColor { get; }

        public Color? BorderColor { get; }

        public int StrokeWidth { get; set; }

        public LocationModel Location { get; set; }

        protected BaseDrawModel(
            Guid id,
            string description,
            TValue radius,
            LocationModel location,
            Color? color,
            Color? borderColor,
            Color? secondaryColor,
            int strokeWidth,
            int opacityPercentage)
        {
            Id = id;
            Description = description;
            Radius = radius;
            Location = location;
            Color = color.SetOpacity(opacityPercentage);
            SecondaryColor = secondaryColor.SetOpacity(opacityPercentage);
            BorderColor = borderColor.SetOpacity(opacityPercentage);
            StrokeWidth = strokeWidth;
        }
    }
}