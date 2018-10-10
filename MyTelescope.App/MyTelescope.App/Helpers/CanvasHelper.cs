namespace MyTelescope.App.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using MyTelescope.Utilities.Helpers;
    using MyTelescope.Utilities.Models;
    using SkiaSharp;
    using SkiaSharp.Views.Forms;
    using Utilities.Enums;
    using Utilities.Helpers;
    using Utilities.Models;

    public static class CanvasHelper
    {
        private static readonly int SunRadius = 25;

        public static void OnPaintSurface(this SKPaintSurfaceEventArgs args, string key)
        {
            var info = args.Info;
            var canvas = args.Surface.Canvas;
            canvas.Clear(SKColors.Black);

            try
            {
                var shapes = CanvasSessionHelper.GetCanvasShapes(key);
                var ojectCollectionLayout = CanvasSessionHelper.GetCanvasObjectCollectionLayout(key);
                Draw(canvas, info, shapes, ojectCollectionLayout);
            }
            catch (Exception exception)
            {
                LogHelper.LogError(exception);
                throw;
            }
        }

        public static void Draw(this SKCanvas canvas, CanvasDrawModel circle)
        {
            if (canvas == null)
            {
                throw new ArgumentException($"{nameof(canvas)} is null.", nameof(canvas));
            }

            try
            {
                var circleColor = circle.Color ?? Color.WhiteSmoke;
                var paint = new SKPaint();

                if (circle.BorderColor.HasValue && circle.StrokeWidth > 0)
                {
                    circleColor = circle.BorderColor.Value;
                    paint.Style = SKPaintStyle.Stroke;
                    paint.StrokeWidth = circle.StrokeWidth;
                }
                else
                {
                    paint.Style = SKPaintStyle.Fill;
                }

                paint.Color = new SKColor(circleColor.R, circleColor.G, circleColor.B, circleColor.A);

                canvas.DrawCircle(circle.CentreX, circle.CentreY, circle.Radius, paint);
            }
            catch (Exception exception)
            {
                LogHelper.LogError(exception);
                throw;
            }
        }

        public static void Draw(
            this SKCanvas canvas,
            SKImageInfo info,
            ICollection<CelestialDrawModel> shapes,
            ObjectCollectionLayout objectCollectionLayout)
        {
            if (!shapes.Any())
            {
                return;
            }

            if (shapes.Count == 2)
            {
                var shapeList = shapes.ToList();
                var origin = shapeList.First();
                var compare = shapeList.Last();
                canvas.DrawCompare(info, origin, compare);
            }
            else
            {
                var orbits = shapes.Where(x => x.BorderColor.HasValue);

                var drawParameters = GetDrawParameters(info, orbits, objectCollectionLayout);
                canvas.DrawOrbits(shapes.Where(x => x.BorderColor.HasValue), drawParameters);
                canvas.DrawBodies(shapes.Where(x => !x.BorderColor.HasValue), drawParameters);
            }
        }

        private static DrawParameterModel GetDrawParameters(SKImageInfo info, IEnumerable<CelestialDrawModel> orbits, ObjectCollectionLayout objectCollectionLayout)
        {
            var maxCanvasWidth = info.Width;
            var maxCanvasHeight = info.Height;
            int maxCanvas;
            var maximumOrbitRadius = orbits.Max(x => x.Radius);

            int centreX;
            int centreY;

            var orientation = DrawOrientation.Horizontal;

            if (objectCollectionLayout == ObjectCollectionLayout.Position)
            {
                centreX = (int)Math.Floor(maxCanvasWidth / (double)2);
                centreY = (int)Math.Floor(maxCanvasHeight / (double)2);

                maxCanvas = IntHelper.GetMin(centreX, centreY);
            }
            else
            {
                if (maxCanvasHeight > maxCanvasWidth)
                {
                    orientation = DrawOrientation.Vertical;
                }

                centreX = orientation == DrawOrientation.Horizontal ? 0 : (int)Math.Floor((double)maxCanvasWidth / 2);
                centreY = orientation == DrawOrientation.Vertical ? maxCanvasHeight : (int)Math.Floor((double)maxCanvasHeight / 2);

                maxCanvas = IntHelper.GetMax(maxCanvasWidth, maxCanvasHeight);
            }

            var orbitFactor = (int)Math.Floor((maxCanvas - SunRadius) / maximumOrbitRadius);

            return new DrawParameterModel(objectCollectionLayout, orientation, centreX, centreY, orbitFactor);
        }

        private static void DrawOrbits(this SKCanvas canvas, IEnumerable<CelestialDrawModel> orbits, DrawParameterModel drawParameters)
        {
            foreach (var orbit in orbits.Where(x => x.Radius > 0))
            {
                var draw =
                    new CanvasDrawModel(
                        orbit.Id,
                        orbit.Description,
                        (int)Math.Ceiling(orbit.Radius * drawParameters.OrbitFactor) + SunRadius,
                        drawParameters.CentreX,
                        drawParameters.CentreY,
                        null,
                        orbit.BorderColor,
                        null,
                        orbit.StrokeWidth);

                canvas.Draw(draw);
            }
        }

        private static void DrawBodies(this SKCanvas canvas, IEnumerable<CelestialDrawModel> bodies, DrawParameterModel drawParameters)
        {
            var orderedBodies = bodies.OrderByDescending(x => x.Radius).ToList();
            var sizes = new[] { SunRadius, 20, 18, 16, 14, 8, 8, 6, 4, 4 };

            for (var index = 0; index < orderedBodies.Count; index++)
            {
                try
                {
                    var body = orderedBodies[index];

                    var centreX = GetCentreValue(drawParameters.CentreX, body.Location, DrawOrientation.Vertical, drawParameters);
                    var centreY = GetCentreValue(drawParameters.CentreY, body.Location, DrawOrientation.Horizontal, drawParameters);

                    var draw =
                        new CanvasDrawModel(
                            body.Id,
                            body.Description,
                            sizes[index],
                            centreX,
                            centreY,
                            body.Color,
                            null,
                            null,
                            0);

                    canvas.Draw(draw);
                }
                catch (ArgumentException exception)
                {
                    LogHelper.LogError(exception);
                }
                catch (Exception exception)
                {
                    LogHelper.LogError(exception);
                }
            }
        }

        private static int GetCentreValue(int canvasCentre, LocationModel location, DrawOrientation neutralOrientation, DrawParameterModel drawParameters)
        {
            if (drawParameters.ObjectCollectionLayout == ObjectCollectionLayout.Position)
            {
                return GetCentreValuePosition(canvasCentre, location, neutralOrientation, drawParameters.OrbitFactor);
            }

            return GetCentreValueDistance(canvasCentre, location, neutralOrientation, drawParameters);
        }

        private static int GetCentreValueDistance(int canvasCentre, LocationModel location, DrawOrientation neutralOrientation, DrawParameterModel drawParameters)
        {
            if (drawParameters.Orientation == neutralOrientation)
            {
                return canvasCentre;
            }

            var orbitRadius = (int) Math.Ceiling(location?.GetOrbitRadius() ?? 0 * drawParameters.OrbitFactor);
            if (orbitRadius > 0)
            {
                orbitRadius += SunRadius;
            }

            if (neutralOrientation == DrawOrientation.Vertical)
            {
                return canvasCentre - orbitRadius;
            }
            
            return canvasCentre + orbitRadius;
        }

        private static int GetCentreValuePosition(int canvasCentre, LocationModel location, DrawOrientation neutralOrientation, int orbitFactor)
        {
            var centreX = location?.X ?? 0;
            var centreY = location?.Y ?? 0;

            var absCentreX = Math.Abs(centreX);
            var absCentreY = Math.Abs(centreY);

            var absCentreTotal = absCentreX + absCentreY;
            
            var factorCentre = absCentreX / absCentreTotal;
            var locationCoordinate = centreX;

            if (neutralOrientation == DrawOrientation.Horizontal)
            {
                factorCentre = absCentreY / absCentreTotal;
                locationCoordinate = centreY;
            }

            var factoredHalfSunSize = (int)Math.Floor(SunRadius * factorCentre);

            var centre = (int)Math.Floor(locationCoordinate * orbitFactor);

            if (centre > 0)
            {
                centre += factoredHalfSunSize;
            }

            if (centre < 0)
            {
                centre -= factoredHalfSunSize;
            }

            centre += canvasCentre;

            return centre;
        }

        private static void DrawCompare(this SKCanvas canvas, SKImageInfo info, CelestialDrawModel origin, CelestialDrawModel compare)
        {
            var originRadius = origin.Radius;
            var compareRadius = compare.Radius;
            var maxCanvasWidth = info.Width;
            var maxCanvasHeight = info.Height;
            var maxRadius = DoubleHelper.GetMax(originRadius, compareRadius);
            var minRadius = DoubleHelper.GetMin(originRadius, compareRadius);

            var divisionFactor = maxCanvasWidth * 0.9 / (minRadius * 4 > maxRadius ? 2 : 1);

            var factor = maxRadius / divisionFactor;

            var originFactoredRadius = (int)DoubleHelper.GetMax(Math.Floor(originRadius / factor), 1);
            var compareFactoredRadius = (int)DoubleHelper.GetMax(Math.Floor(compareRadius / factor), 1);

            var originCentreX = GetCentreX(maxCanvasWidth, originFactoredRadius, true);
            var compareCentreX = GetCentreX(maxCanvasWidth, compareFactoredRadius, false);

            var centreY = maxCanvasHeight / 2;

            var originDraw = new CanvasDrawModel(origin.Id, origin.Description, originFactoredRadius, originCentreX, centreY, origin.Color, null, null, 0);
            var compareDraw = new CanvasDrawModel(compare.Id, compare.Description, compareFactoredRadius, compareCentreX, centreY, compare.Color, null, null, 0);

            canvas.Draw(originDraw);
            canvas.Draw(compareDraw);
        }

        private static int GetCentreX(int canvasWidth, int factoredRadius, bool lefthand)
        {
            var halfCanvasWidth = (int)DoubleHelper.GetMax((int)Math.Floor(canvasWidth * 0.5), 1);

            if (factoredRadius >= halfCanvasWidth)
            {
                return lefthand
                    ? (int)Math.Floor(canvasWidth * 0.45) - factoredRadius
                    : (int)Math.Floor(canvasWidth * 0.55) + factoredRadius;
            }

            if (factoredRadius * 2 < halfCanvasWidth)
            {
                return lefthand
                    ? (int)Math.Floor(canvasWidth * 0.25)
                    : (int)Math.Floor(canvasWidth * 0.75);
            }

            return lefthand
                ? 0
                : canvasWidth;
        }
    }
}