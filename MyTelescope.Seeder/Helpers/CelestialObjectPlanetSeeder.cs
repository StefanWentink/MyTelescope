namespace MyTelescope.Seeder.Helpers
{
    using SolarSystem.Helpers.Seeder;
    using SolarSystem.Models.CelestialObject;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Utilities.Interfaces.Connector;

    public class CelestialObjectPlanetSeeder : BaseSeeder<CelestialObjectModel, string>
    {
        public CelestialObjectPlanetSeeder(
            IContextConnector<CelestialObjectModel> connector)
            : base(connector)
        {
        }

        protected override List<Expression<Func<CelestialObjectModel, bool>>> GetBatchExpression()
        {
            return new List<Expression<Func<CelestialObjectModel, bool>>> { x => true };
        }

        protected override List<CelestialObjectModel> SeedList(Expression<Func<CelestialObjectModel, bool>> batchExpression)
        {
            return CelestialObjectSeedHelper.GetPlanets().Where(batchExpression.Compile()).ToList();
        }

        protected override Func<CelestialObjectModel, string> DuplicateCheckFunction
        {
            get { return x => x.Code; }
        }
    }
}