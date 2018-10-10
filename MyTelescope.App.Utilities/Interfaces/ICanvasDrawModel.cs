namespace MyTelescope.App.Utilities.Interfaces
{
    using System;
    using System.Drawing;

    public interface ICanvasDrawModel
    {
        int Radius { get; }

        Guid Id { get; }

        string Description { get; }

        Color? Color { get; }

        Color? SecondaryColor { get; }

        Color? BorderColor { get; }

        int CentreX { get; set; }

        int CentreY { get; set; }
    }
}
