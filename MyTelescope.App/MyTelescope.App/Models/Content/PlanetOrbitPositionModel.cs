namespace MyTelescope.App.Pages.Content
{
    using DataLayer.Interfaces;
    using Models.Base;
    using MyTelescope.Utilities.Helpers;
    using SolarSystem.Models.CelestialObject;
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using ViewModels.Models.Item;

    public class PlanetOrbitPositionModel : DetailOverViewPageModel<CelestialObjectPositionViewModel, CelestialObjectPositionModel>
    {
        public PlanetOrbitPositionModel(IStaticDataLoader<CelestialObjectPositionViewModel, CelestialObjectPositionModel> dataLoader)
            : base(dataLoader)
        {
        }

        public override void SetModel(CelestialObjectPositionModel model)
        {
            if (!GuidHelper.GuidIsNullOrEmpty(model?.Id) && model != Model)
            {
                var modelReferenceYear = model?.ReferenceDate.Year ?? DateTime.Today.Year;

                if (ReferenceYear == default(int) || ReferenceYear != modelReferenceYear)
                {
                    ReferenceYear = modelReferenceYear;
                }
                else
                {
                    base.SetModel(model);
                }
            }
        }

        private const int _minimumYear = 1950;

        private const int _maximumYear = 2049;

        public ObservableCollection<int> YearCollection { get; } = new ObservableCollection<int>(Enumerable.Range(_minimumYear, _maximumYear));

        private readonly object _referenceYearLock = new object();

        private int _referenceYear;

        public int ReferenceYear
        {
            get
            {
                lock (_referenceYearLock)
                {
                    return _referenceYear;
                }
            }

            set
            {
                lock (_referenceYearLock)
                {
                    if (_referenceYear != value)
                    {
                        _referenceYear = value;

                        if (_referenceYear > _maximumYear)
                        {
                            _referenceYear = _minimumYear;
                        }
                        else if (_referenceYear < _minimumYear)
                        {
                            _referenceYear = _maximumYear;
                        }

                        if (_referenceYear != default(int))
                        {
                            RaisePropertyChanged(nameof(ReferenceYear));

                            var positionModel = new CelestialObjectPositionModel(
                                    Model.CelestialObjectId,
                                    new DateTimeOffset(ReferenceYear, 1, 1, 0, 0, 0, TimeSpan.Zero),
                                    new DateTimeOffset(ReferenceYear, 12, 31, 0, 0, 0, TimeSpan.Zero));

                            SetModel(positionModel);
                        }
                    }
                }
            }
        }
    }
}