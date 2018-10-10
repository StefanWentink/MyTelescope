namespace MyTelescope.App.Pages.Content
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;
    using Converter;
    using DataLayer.Enums;
    using DataLayer.Interfaces;
    using Extensions;
    using Helpers;
    using Models.Base;
    using MyTelescope.Utilities.Helpers;
    using SolarSystem.Constants;
    using SolarSystem.Models.CelestialObject;
    using Utilities.Enums;
    using Utilities.EventArgs;
    using Utilities.Helpers;
    using Utilities.Models;
    using ViewModels.Constants;
    using ViewModels.Models;
    using ViewModels.Models.Item;
    using Xamarin.Forms;

    public class SolarSystemPageModel : CanvasPageModel<CelestialObjectPositionViewModel, CelestialObjectPositionModel>
    {
        public override string CanvasViewKey => ModelHelper.GetName(GetType().Name);

        public SolarSystemPageModel(
            IHttpDataLoader<CelestialObjectPositionViewModel, CelestialObjectPositionModel> dataLoader,
            IHttpDataLoader<CelestialObjectViewModel, CelestialObjectModel> celestialObjectDataLoader)
            : base(dataLoader)
        {
            CelestialObjectDataLoader = celestialObjectDataLoader;
            CelestialObjectDataLoader.CollectionFetchedEvent += CelestialObjectDataLoaderOnCollectionFetchedEvent;
            CelestialObjectDataLoader.EndOfCollectionEvent += CelestialObjectDataLoaderOnEndOfCollectionEvent;
            CelestialObjectDataLoader.LoadAsync(DataLoading.Refresh, SolarSystem.Helpers.Seeder.CelestialObjectSeedHelper.GetSun());
        }

        public override void SetModel(CelestialObjectPositionModel model)
        {
            if (!GuidHelper.GuidIsNullOrEmpty(model?.Id) && model != Model)
            {
                var modelReferenceDate = model?.ReferenceDate.DateTime ?? DateTime.Today;

                if (ReferenceDate == default(DateTime) || ReferenceDate != modelReferenceDate)
                {
                    ReferenceDate = modelReferenceDate;
                }
                else
                {
                    base.SetModel(model);
                }
            }
        }

        private async Task Preload(DateTime referenceDate)
        {
            var loadThreshold = 10 * DaySkip;

            var loadBoundary = IntHelper.GetMax(10 * DaySkip, DataConnectionHelper.GetPositionDaysForDataConnection());

            if (loadThreshold <= 0)
            {
                loadThreshold = loadBoundary;
            }

            if (referenceDate.AddDays(loadThreshold) > MaximumDateLoaded)
            {
                await PreloadDays(MaximumDateLoaded, loadBoundary).ConfigureAwait(false);
            }

            if (referenceDate.AddDays(-loadThreshold) < MinimumDateLoaded)
            {
                await PreloadDays(MinimumDateLoaded, -loadBoundary).ConfigureAwait(false);
            }
        }

        private async Task PreloadDays(DateTime referenceDate, int days)
        {
            if (days < 0)
            {
                MinimumDateLoaded = referenceDate.AddDays(days);
            }

            if (days > 0)
            {
                MaximumDateLoaded = referenceDate.AddDays(days);
            }

            var start = IntHelper.GetMin(0, days);
            var end = IntHelper.GetMax(0, days);

            for (var i = start; i <= end; i++)
            {
                if (i != 0)
                {
                    var loadDate = referenceDate.AddDays(i).GetClosestUtcZeroTime();
                    var model = SolarSystem.Helpers.Seeder.CelestialObjectSeedHelper.GetSunPosition(loadDate);

                    await DataLoader.LoadAsync(DataLoading.Preload, model).ConfigureAwait(false);
                }
            }
        }

        private readonly object _minimumDateLoadedLock = new object();

        private DateTime _minimumDateLoaded = DateTime.Today;

        public DateTime MinimumDateLoaded
        {
            get
            {
                lock (_minimumDateLoadedLock)
                {
                    return _minimumDateLoaded;
                }
            }

            set
            {
                lock (_minimumDateLoadedLock)
                {
                    _minimumDateLoaded = value;
                }
            }
        }

        private readonly object _maximumDateLoadedLock = new object();

        private DateTime _maximumDateLoaded = DateTime.Today;

        public DateTime MaximumDateLoaded
        {
            get
            {
                lock (_maximumDateLoadedLock)
                {
                    return _maximumDateLoaded;
                }
            }

            set
            {
                lock (_maximumDateLoadedLock)
                {
                    _maximumDateLoaded = value;
                }
            }
        }

        protected IDataLoader<CelestialObjectViewModel, CelestialObjectModel> CelestialObjectDataLoader { get; }

        private static object _celestialObjectCollectionLock = new object();

        protected ObservableCollection<CelestialObjectViewModel> CelestialObjectCollection { get; }
            = new ObservableCollection<CelestialObjectViewModel>();

        protected override void CollectionSet()
        {
            AddObjectCollectionLayoutModel(SelectListConstants.ObjectCollectionLayoutPosition, ObjectCollectionLayouts.Count);
        }

        private void AddObjectCollectionLayoutModel(ObjectCollectionLayoutModel objectCollectionLayoutModel, int index)
        {
            if (ObjectCollectionLayouts.Any(x => x.Value == objectCollectionLayoutModel.Value))
            {
                PrepareShapes();
            }
            else
            {
                ObjectCollectionLayouts.InsertOnApplicationThread(
                    objectCollectionLayoutModel,
                    _objectCollectionLayoutsLock,
                    ObjectCollectionLayoutsSet,
                    nameof(ObjectCollectionLayouts),
                    index,
                    null);
            }
        }

        private void ObjectCollectionLayoutsSet(string propertyName)
        {
            RaisePropertyChanged(propertyName);

            if (ObjectCollectionLayoutSelectedIndex < ObjectCollectionLayouts.Count - 1)
            {
                ObjectCollectionLayoutSelectedIndex = ObjectCollectionLayouts.Count - 1;
            }
        }

        private void CelestialObjectDataLoaderOnCollectionFetchedEvent(object sender, CollectionFetchedEventArgs<CelestialObjectViewModel> collectionFetchedEventArgs)
        {
            ThreadHelper.SetOnApplicationThread(
                CelestialObjectCollection, 
                collectionFetchedEventArgs.Models, 
                _celestialObjectCollectionLock, 
                RaisePropertyChanged, 
                nameof(CelestialObjectCollection),
                null);
        }

        private void CelestialObjectDataLoaderOnEndOfCollectionEvent(object sender, EndOfCollectionEventArgs endOfCollectionEventArgs)
        {
            Task.Run(() => ProcessCelestialObjects(endOfCollectionEventArgs.Count)).ConfigureAwait(false);
        }

        private async Task ProcessCelestialObjects(int collectionCount)
        {
            while (CelestialObjectCollection.Count < collectionCount)
            {
                await Task.Delay(10).ConfigureAwait(false);
            }

            AddObjectCollectionLayoutModel(SelectListConstants.ObjectCollectionLayoutDistance, 0);

            var objectCollectionOptions = new ObservableCollection<ObjectCollectionOptionModel>
            {
                SelectListConstants.ObjectCollectionOptionAll,
                SelectListConstants.ObjectCollectionOptionInner,
                SelectListConstants.ObjectCollectionOptionOuter
            };

            ObjectCollectionOptions.SetOnApplicationThread(
                objectCollectionOptions, 
                _objectCollectionOptionsLock, 
                ObjectCollectionOptionsSet, 
                nameof(ObjectCollectionOptions),
                null);
        }

        private void ObjectCollectionOptionsSet(string propertyName)
        {
            RaisePropertyChanged(propertyName);
            ObjectCollectionOptionSelectedIndex = 0;
        }

        private int _objectCollectionLayoutSelectedIndex = -1;

        private readonly object _objectCollectionLayoutsLock = new object();

        public ObservableCollection<ObjectCollectionLayoutModel> ObjectCollectionLayouts { get; set; } = new ObservableCollection<ObjectCollectionLayoutModel>();

        public int ObjectCollectionLayoutSelectedIndex
        {
            get => _objectCollectionLayoutSelectedIndex;
            set
            {
                if (_objectCollectionLayoutSelectedIndex != value)
                {
                    _objectCollectionLayoutSelectedIndex = value;
                    RaisePropertyChanged(nameof(ObjectCollectionLayoutSelectedIndex));
                    if (!new TimeLapseVisibleConverter().ConvertSelectedIndex(_objectCollectionLayoutSelectedIndex))
                    {
                        DaySkip = 0;
                    }

                    PrepareShapes();
                }
            }
        }

        private int _objectCollectionOptionSelectedIndex = -1;

        private readonly object _objectCollectionOptionsLock = new object();

        public ObservableCollection<ObjectCollectionOptionModel> ObjectCollectionOptions { get; set; }
            = new ObservableCollection<ObjectCollectionOptionModel>();

        public int ObjectCollectionOptionSelectedIndex
        {
            get => _objectCollectionOptionSelectedIndex;
            set
            {
                if (_objectCollectionOptionSelectedIndex != value)
                {
                    _objectCollectionOptionSelectedIndex = value;
                    RaisePropertyChanged(nameof(ObjectCollectionOptionSelectedIndex));
                    PrepareShapes();
                }
            }
        }

        private readonly DateTime _minimumDate = new DateTime(1950, 1, 1);

        private readonly DateTime _maximumDate = new DateTime(2049, 12, 31);

        private readonly object _referenceDateLock = new object();

        private DateTime _referenceDate;

        public DateTime ReferenceDate
        {
            get
            {
                lock (_referenceDateLock)
                {
                    return _referenceDate;
                }
            }

            set
            {
                lock (_referenceDateLock)
                {
                    if (_referenceDate != value)
                    {
                        _referenceDate = value;

                        if (_referenceDate > _maximumDate)
                        {
                            _referenceDate = _minimumDate;
                        }
                        else if (_referenceDate < _minimumDate)
                        {
                            _referenceDate = _maximumDate;
                        }

                        if (_referenceDate != default(DateTime))
                        {
                            RaisePropertyChanged(nameof(ReferenceDate));

                            Task.Run(() => Preload(_referenceDate));

                            var positionViewModel = SolarSystem.Helpers.Seeder.CelestialObjectSeedHelper.GetSunPosition(_referenceDate.GetClosestUtcZeroTime());

                            SetModel(positionViewModel);
                        }
                    }
                }
            }
        }

        private Func<CelestialObjectPositionViewModel, bool> GetCollectionOptionOrbitExpression(int optionSelectedIndex)
        {
            switch (ObjectCollectionOptions[optionSelectedIndex].Value)
            {
                case ObjectCollectionOption.Inner:
                    return x => x.Model.AverageCentricDistance < 2;
                case ObjectCollectionOption.Outer:
                    return x => x.Model.AverageCentricDistance >= 2 || x.Model.AverageCentricDistance.EqualsWithinTolerance(0, 6);
                case ObjectCollectionOption.All:
                    return x => x.Model.AverageCentricDistance >= 0;
                default:
                    return x => true;
            }
        }

        private Dictionary<Guid, CelestialDrawModel> _celestialDrawBodies;
        private Dictionary<Guid, CelestialDrawModel> _celestialDrawOrbits;

        protected override void PrepareShapes()
        {
            if (ObjectCollectionLayoutSelectedIndex >= 0 && ObjectCollectionOptionSelectedIndex >= 0 && Collection.Any())
            {
                var initialLoad = _celestialDrawBodies == null;

                if (initialLoad)
                {
                    _celestialDrawBodies = new Dictionary<Guid, CelestialDrawModel>();
                    _celestialDrawOrbits = new Dictionary<Guid, CelestialDrawModel>();

                    if (CelestialObjectCollection.SingleOrDefault(x => x.Code == CelestialObjectConstants.Sun) == null)
                    {
                        CelestialObjectCollection.Add(new CelestialObjectViewModel(SolarSystem.Helpers.Seeder.CelestialObjectSeedHelper.GetSun()));
                    }

                    foreach (var celestialObject in CelestialObjectCollection)
                    {
                        var celestialPosition = Collection.SingleOrDefault(x => x.Model.CelestialObjectId == celestialObject.Id);

                        var celestialObjectDraw = celestialPosition.ToCelestialBodyDrawModel(celestialObject);
                        _celestialDrawBodies.Add(celestialObjectDraw.Id, celestialObjectDraw);

                        if (celestialPosition != null)
                        {
                            var orbitDraw = celestialPosition.ToCelestialOrbitDrawModel(celestialObject);
                            _celestialDrawOrbits.Add(celestialObjectDraw.Id, orbitDraw);
                        }
                    }
                }

                var shapes = new List<CelestialDrawModel>();

                foreach (var celestialObjectPosition in Collection.Where(x => GetCollectionOptionOrbitExpression(_objectCollectionOptionSelectedIndex)(x)).ToList())
                {
                    if (!initialLoad)
                    {
                        _celestialDrawOrbits[celestialObjectPosition.Model.CelestialObjectId].Location = celestialObjectPosition.Model.Location;
                        _celestialDrawBodies[celestialObjectPosition.Model.CelestialObjectId].Location = celestialObjectPosition.Model.Location;
                    }

                    shapes.Add(_celestialDrawOrbits[celestialObjectPosition.Model.CelestialObjectId]);
                    shapes.Add(_celestialDrawBodies[celestialObjectPosition.Model.CelestialObjectId]);
                }

                var objectCollectionLayout = ObjectCollectionLayouts[ObjectCollectionLayoutSelectedIndex].Value;

                SetShapes(shapes, objectCollectionLayout);
            }
        }

        private const int FastMilliseconds = 400;

        private const int NormalMilliseconds = 500;

        private const int NormalSkipdays = 1;

        private const int FastSkipdays = 3;

        public Command ForwardsCommand
        {
            get { return new Command(async () => { await Skip(NormalSkipdays, NormalMilliseconds).ConfigureAwait(false); }); }
        }

        public Command FastForwardsCommand
        {
            get { return new Command(async () => { await Skip(FastSkipdays, FastMilliseconds).ConfigureAwait(false); }); }
        }

        public Command BackwardsCommand
        {
            get { return new Command(async () => { await Skip(-NormalSkipdays, NormalMilliseconds).ConfigureAwait(false); }); }
        }

        public Command FastBackwardsCommand
        {
            get { return new Command(async () => { await Skip(-FastSkipdays, FastMilliseconds).ConfigureAwait(false); }); }
        }

        private async Task Skip(int daySkip, int millisecondDelay)
        {
            var startSkip = DaySkip == 0;

            MillisecondDelay = millisecondDelay;
            DaySkip = daySkip;

            if (startSkip)
            {
                await SkipDelay().ConfigureAwait(false);
            }
        }

        private static readonly object MillisecondDelayLock = new object();

        private int _millisecondDelay;

        private int MillisecondDelay
        {
            get
            {
                lock (MillisecondDelayLock)
                {
                    return _millisecondDelay;
                }
            }

            set
            {
                lock (MillisecondDelayLock)
                {
                    _millisecondDelay = value;
                }
            }
        }

        private static readonly object DaySkipLock = new object();

        private int _daySkip;

        private int DaySkip
        {
            get
            {
                lock (DaySkipLock)
                {
                    return _daySkip;
                }
            }

            set
            {
                lock (DaySkipLock)
                {
                    _daySkip = _daySkip + value == 0
                        ? 0
                        : value;
                }
            }
        }

        private async Task SkipDelay()
        {
            if (MillisecondDelay > 0 && DaySkip != 0)
            {
                await Task.Delay(MillisecondDelay).ContinueWith(x => SkipSetReferenceDate()).ConfigureAwait(false);
            }
        }

        private async Task SkipSetReferenceDate()
        {
            if (MillisecondDelay > 0 && DaySkip != 0)
            {
                await Task.Run(() => ReferenceDate = ReferenceDate.AddDays(DaySkip))
                    .ContinueWith(x => SkipDelay()).ConfigureAwait(false);
            }
        }

        protected override void ViewIsDisappearing(object sender, EventArgs e)
        {
            DaySkip = 0;

            base.ViewIsDisappearing(sender, e);
        }
    }
}