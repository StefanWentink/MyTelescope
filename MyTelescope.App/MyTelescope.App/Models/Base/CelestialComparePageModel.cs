namespace MyTelescope.App.Models.Base
{
    using MyTelescope.Data.Loader.Interfaces;
    using Extensions;
    using Interfaces;
    using MyTelescope.App.Helpers;
    using SolarSystem.Models.CelestialObject;
    using System.Collections.Generic;
    using System.Linq;
    using Utilities.Models;
    using ViewModels.Models.Item;

    public abstract class CelestialComparePageModel :
        CanvasPageModel<CelestialObjectViewModel, CelestialObject>,
        IDrawablePageModel
    {
        protected CelestialComparePageModel(IHttpDataLoader<CelestialObjectViewModel, CelestialObject> dataLoader)
            : base(dataLoader)
        {
        }

        protected override void CollectionSet()
        {
            ShowPageDefaultSelection();
        }

        private void ShowPageDefaultSelection()
        {
            if (Collection?.Any() == true)
            {
                if (OriginDrawSelectedIndex < 0)
                {
                    OriginDrawSelectedIndex = 0;
                }

                if (CompareDrawSelectedIndex < 0 && Collection.Count == 2)
                {
                    CompareDrawSelectedIndex = 1;
                }
            }
        }

        private int _originDrawSelectedIndex = -1;

        public int OriginDrawSelectedIndex
        {
            get => _originDrawSelectedIndex;
            set
            {
                if (_originDrawSelectedIndex != value)
                {
                    _originDrawSelectedIndex = value;
                    ThreadHelper.RaiseOnApplicationThread(RaisePropertyChanged, nameof(OriginDrawSelectedIndex));
                    PrepareShapes();
                }
            }
        }

        private int _compareDrawSelectedIndex = -1;

        public int CompareDrawSelectedIndex
        {
            get => _compareDrawSelectedIndex;
            set
            {
                if (_compareDrawSelectedIndex != value)
                {
                    _compareDrawSelectedIndex = value;
                    ThreadHelper.RaiseOnApplicationThread(RaisePropertyChanged, nameof(CompareDrawSelectedIndex));
                    PrepareShapes();
                }
            }
        }

        protected override void PrepareShapes()
        {
            if (_originDrawSelectedIndex >= 0 && _compareDrawSelectedIndex >= 0)
            {
                var shapes = new List<CelestialDrawModel>
                {
                    DrawExtensions.ToCelestialCompareDrawModelFunction.Invoke(Collection[_originDrawSelectedIndex]),
                    DrawExtensions.ToCelestialCompareDrawModelFunction.Invoke(Collection[_compareDrawSelectedIndex])
                };

                SetShapes(shapes);
            }
        }
    }
}