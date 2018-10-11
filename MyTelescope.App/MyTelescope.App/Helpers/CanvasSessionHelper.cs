namespace MyTelescope.App.Helpers
{
    using Models.Canvas;
    using System.Collections.Generic;
    using System.Linq;
    using Utilities.Enums;
    using Utilities.Models;

    public static class CanvasSessionHelper
    {
        private static readonly object ObjectCollectionLayoutLock = new object();

        private static ObjectCollectionLayout _objectCollectionLayout = ObjectCollectionLayout.Position;

        public static void SetCanvasObjectCollectionLayout(string key, ObjectCollectionLayout objectCollectionLayout)
        {
            lock (ObjectCollectionLayoutLock)
            {
                _objectCollectionLayout = objectCollectionLayout;
            }
        }

        public static ObjectCollectionLayout GetCanvasObjectCollectionLayout(string key)
        {
            lock (ObjectCollectionLayoutLock)
            {
                return _objectCollectionLayout;
            }
        }

        private static readonly object ShapeCollectionLock = new object();

        private static readonly Dictionary<string, List<CelestialDrawModel>> CanvasShapes = new Dictionary<string, List<CelestialDrawModel>>();

        public static void SetCanvasShapes(string key, List<CelestialDrawModel> shapes)
        {
            lock (ShapeCollectionLock)
            {
                if (!string.IsNullOrWhiteSpace(key))
                {
                    var pushShapes = shapes ?? new List<CelestialDrawModel>();

                    if (CanvasShapes.ContainsKey(key))
                    {
                        CanvasShapes[key] = pushShapes;
                    }
                    else
                    {
                        CanvasShapes.Add(key, pushShapes);
                    }

                    if (pushShapes.Count > 0)
                    {
                        InvalidateSurface(key);
                    }
                }
            }
        }

        public static List<CelestialDrawModel> GetCanvasShapes(string key)
        {
            lock (ShapeCollectionLock)
            {
                if (!string.IsNullOrWhiteSpace(key) && CanvasShapes.ContainsKey(key))
                {
                    return CanvasShapes[key];
                }
            }

            return new List<CelestialDrawModel>();
        }

        public static void RemoveCanvasShapes(string key)
        {
            lock (ShapeCollectionLock)
            {
                if (CanvasShapes.ContainsKey(key))
                {
                    CanvasShapes.Remove(key);
                }
            }
        }

        public static void ClearShapes(string key)
        {
            lock (ShapeCollectionLock)
            {
                if (CanvasShapes.ContainsKey(key))
                {
                    SetCanvasShapes(key, null);
                }
            }
        }

        private static readonly object CanvasLock = new object();

        private static readonly Dictionary<string, BindableCanvasView> CanvasView = new Dictionary<string, BindableCanvasView>();

        public static void SetCanvasView(string key, BindableCanvasView canvas)
        {
            lock (CanvasLock)
            {
                if (!string.IsNullOrWhiteSpace(key))
                {
                    if (CanvasView.ContainsKey(key))
                    {
                        CanvasView[key] = canvas;
                        return;
                    }

                    CanvasView.Add(key, canvas);
                }
            }
        }

        private static BindableCanvasView GetCanvasView(string key)
        {
            lock (CanvasLock)
            {
                if (!string.IsNullOrWhiteSpace(key) && CanvasView.ContainsKey(key))
                {
                    return CanvasView[key];
                }
            }

            return null;
        }

        public static void RemoveCanvasView(string key)
        {
            lock (CanvasLock)
            {
                if (CanvasView.ContainsKey(key))
                {
                    CanvasView.Remove(key);
                }
            }
        }

        private static void InvalidateSurface(string key)
        {
            GetCanvasView(key)?.InvalidateSurface();
        }
    }
}