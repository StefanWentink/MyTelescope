namespace MyTelescope.App.Models.Canvas
{
    using Helpers;
    using MyTelescope.Core.Utilities.Helpers;
    using SkiaSharp.Views.Forms;
    using System.Collections.ObjectModel;
    using System.Linq;
    using Utilities.Helpers;
    using Utilities.Models;
    using Xamarin.Forms;

    public class BindableCanvasView : SKCanvasView
    {
        public static readonly BindableProperty CanvasViewKeyProperty =
            BindableProperty.Create(
                "CanvasViewKey",
                typeof(string),
                typeof(BindableCanvasView),
                string.Empty,
                defaultBindingMode: BindingMode.TwoWay,
                propertyChanged: RedrawCanvas);

        public string CanvasViewKey
        {
            get => (string)GetValue(CanvasViewKeyProperty);
            set => SetValue(CanvasViewKeyProperty, value);
        }

        public static readonly BindableProperty ShapesProperty =
            BindableProperty.Create(
                "Shapes",
                typeof(ObservableCollection<CelestialDrawModel>),
                typeof(BindableCanvasView),
                new ObservableCollection<CelestialDrawModel>(),
                defaultBindingMode: BindingMode.TwoWay,
                propertyChanged: RedrawCanvas);

        public ObservableCollection<CelestialDrawModel> Shapes
        {
            get => (ObservableCollection<CelestialDrawModel>)GetValue(ShapesProperty);
            set => SetValue(ShapesProperty, value);
        }

        private static void RedrawCanvas(BindableObject bindable, object oldvalue, object newvalue)
        {
            if (bindable is BindableCanvasView bindableCanvas)
            {
                if (newvalue is string newCanvasViewKey && oldvalue is string oldCanvasViewKey)
                {
                    if (!string.IsNullOrWhiteSpace(newCanvasViewKey) && oldCanvasViewKey != newCanvasViewKey)
                    {
                        CanvasSessionHelper.SetCanvasView(newCanvasViewKey, bindableCanvas);
                    }

                    if (!string.IsNullOrWhiteSpace(oldCanvasViewKey))
                    {
                        CanvasSessionHelper.RemoveCanvasView(oldCanvasViewKey);
                    }
                }

                if (newvalue is ObservableCollection<CelestialDrawModel> shapes)
                {
                    foreach (var shape in shapes)
                    {
                        LogHelper.LogDebug(shape.Description);
                    }

                    if (shapes.Any())
                    {
                        bindableCanvas?.InvalidateSurface();
                    }
                }

                bindableCanvas?.InvalidateSurface();
            }
        }
    }
}