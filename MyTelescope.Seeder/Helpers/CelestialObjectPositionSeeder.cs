namespace MyTelescope.Seeder.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using SolarSystem.Constants;
    using SolarSystem.Enums;
    using SolarSystem.Extensions;
    using SolarSystem.Models.CelestialObject;
    using SolarSystem.Models.Keplerian;
    using Utilities.Interfaces.Connector;

    public class CelestialObjectPositionSeeder : BaseSeeder<CelestialObjectPositionModel, object>
    {
        private Dictionary<string, Guid> CelestialObjectDictionary { get; }

        private List<CelestialObjectPositionModel> CelestialObjectPositions { get; }

        private List<CelestialObjectPositionModel> _earthPositions;

        private Dictionary<DateTimeOffset, CelestialObjectPositionModel> _earthLocations;

        public CelestialObjectPositionSeeder(IContextConnector<CelestialObjectPositionModel> connector, List<CelestialObjectModel> celestialObjects)
            : base(connector)
        {
            CelestialObjectDictionary = celestialObjects.ToDictionary(x => x.Code, x => x.Id);
            CelestialObjectPositions = celestialObjects.Select(x => new CelestialObjectPositionModel(x.Id, DateTimeOffset.Now, null,0, 0)).ToList();
        }

        private readonly object _earthPositionLock = new object();

        protected override List<Expression<Func<CelestialObjectPositionModel, bool>>> GetBatchExpression()
        {
            var result = new List<Expression<Func<CelestialObjectPositionModel, bool>>>();

            foreach (var celestialObjectId in CelestialObjectDictionary.Values)
            {
                result.Add(x => x.CelestialObjectId == celestialObjectId);
            }

            return result;
        }

        protected override List<CelestialObjectPositionModel> SeedList(Expression<Func<CelestialObjectPositionModel, bool>> batchExpression)
        {
            var result = new List<CelestialObjectPositionModel>();

            lock (_earthPositionLock)
            {
                if (_earthPositions == null)
                {
                    _earthPositions = SeedPlanet(CelestialObject.Earth, null);
                    _earthLocations = _earthPositions.ToDictionary(x => x.ReferenceDate, x => x);
                }
            }

            foreach(var celestialObjectPosition in CelestialObjectPositions.Where(batchExpression.Compile()))
            {
                var celestialObjectCode = CelestialObjectDictionary.Single(x => x.Value == celestialObjectPosition.CelestialObjectId).Key;
                var celestialObject = CelestialObjectExtensions.ToEnum(celestialObjectCode);

                result.AddRange(SeedPlanet(celestialObject, _earthLocations));
            }

            return result;
        }

        private List<CelestialObjectPositionModel> SeedPlanet(CelestialObject planet, Dictionary<DateTimeOffset, CelestialObjectPositionModel> earthPositions)
        {
            var result = new List<CelestialObjectPositionModel>();
            var celestialObjectId = CelestialObjectDictionary[planet.ToConstant()];
            var referenceDate = new DateTimeOffset(1950, 1, 1, 0, 0, 0, TimeSpan.Zero);
            var endDate = new DateTimeOffset(2100, 1, 1, 0, 0, 0, TimeSpan.Zero);
            var keplerianModel = KeplerianValueConstants.PlanetKeplerianModels.Single(x => x.SolarSystemObject == planet);

            while (referenceDate <= endDate)
            {
                var dateModel = new KeplerianDateModel(referenceDate, keplerianModel);
                var calculationModel = new KeplerianCalculationModel(dateModel);

                if (earthPositions != null)
                {
                    var earthPosition = earthPositions[referenceDate];
                    calculationModel.SetEarthPosition(earthPosition.Location, earthPosition.CentricDistance);
                }

                var positionModel = planet == CelestialObject.Earth
                    ? new CelestialObjectPositionModel(celestialObjectId, referenceDate, calculationModel.Location, 1, calculationModel.CentricDistance)
                    : new CelestialObjectPositionModel(celestialObjectId, referenceDate, calculationModel);

                result.Add(positionModel);

                referenceDate = referenceDate.AddDays(1);
            }

            return result;
        }

        protected override Func<CelestialObjectPositionModel, object> DuplicateCheckFunction
        {
            get { return x => new { x.CelestialObjectId, x.ReferenceDate }; }
        }
    }
}
