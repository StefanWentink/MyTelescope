namespace MyTelescope.Seeder.Helpers
{
    using SolarSystem.Constants;
    using SolarSystem.Enums;
    using SolarSystem.Extensions;
    using SolarSystem.Models.CelestialObject;
    using SolarSystem.Models.Keplerian;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Utilities.Interfaces.Connector;

    public class CelestialObjectPositionSeeder : BaseSeeder<CelestialObjectPosition, object>
    {
        private Dictionary<string, Guid> CelestialObjectDictionary { get; }

        private List<CelestialObjectPosition> CelestialObjectPositions { get; }

        private List<CelestialObjectPosition> _earthPositions;

        private Dictionary<DateTimeOffset, CelestialObjectPosition> _earthLocations;

        public CelestialObjectPositionSeeder(IContextConnector<CelestialObjectPosition> connector, List<CelestialObject> celestialObjects)
            : base(connector)
        {
            CelestialObjectDictionary = celestialObjects.ToDictionary(x => x.Code, x => x.Id);
            CelestialObjectPositions = celestialObjects.Select(x => new CelestialObjectPosition(x.Id, DateTimeOffset.Now, null, 0, 0)).ToList();
        }

        private readonly object _earthPositionLock = new object();

        protected override List<Expression<Func<CelestialObjectPosition, bool>>> GetBatchExpression()
        {
            var result = new List<Expression<Func<CelestialObjectPosition, bool>>>();

            foreach (var celestialObjectId in CelestialObjectDictionary.Values)
            {
                result.Add(x => x.CelestialObjectId == celestialObjectId);
            }

            return result;
        }

        protected override List<CelestialObjectPosition> SeedList(Expression<Func<CelestialObjectPosition, bool>> batchExpression)
        {
            var result = new List<CelestialObjectPosition>();

            lock (_earthPositionLock)
            {
                if (_earthPositions == null)
                {
                    _earthPositions = SeedPlanet(Celestial.Earth, null);
                    _earthLocations = _earthPositions.ToDictionary(x => x.ReferenceDate, x => x);
                }
            }

            foreach (var celestialObjectPosition in CelestialObjectPositions.Where(batchExpression.Compile()))
            {
                var celestialObjectCode = CelestialObjectDictionary.Single(x => x.Value == celestialObjectPosition.CelestialObjectId).Key;
                var celestialObject = CelestialObjectExtensions.ToEnum(celestialObjectCode);

                result.AddRange(SeedPlanet(celestialObject, _earthLocations));
            }

            return result;
        }

        private List<CelestialObjectPosition> SeedPlanet(Celestial planet, Dictionary<DateTimeOffset, CelestialObjectPosition> earthPositions)
        {
            var result = new List<CelestialObjectPosition>();
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

                var positionModel = planet == Celestial.Earth
                    ? new CelestialObjectPosition(celestialObjectId, referenceDate, calculationModel.Location, 1, calculationModel.CentricDistance)
                    : new CelestialObjectPosition(celestialObjectId, referenceDate, calculationModel);

                result.Add(positionModel);

                referenceDate = referenceDate.AddDays(1);
            }

            return result;
        }

        protected override Func<CelestialObjectPosition, object> DuplicateCheckFunction
        {
            get { return x => new { x.CelestialObjectId, x.ReferenceDate }; }
        }
    }
}