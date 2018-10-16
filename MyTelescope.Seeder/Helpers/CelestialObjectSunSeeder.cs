namespace MyTelescope.Seeder.Helpers
{
    using SolarSystem.Helpers.Seeder;
    using SolarSystem.Models.CelestialObject;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Utilities.Interfaces.Connector;

    public class CelestialObjectSunSeeder : BaseSeeder<CelestialObject, string>
    {
        public CelestialObjectSunSeeder(
            IContextConnector<CelestialObject> connector)
            : base(connector)
        {
        }

        private static List<CelestialObject> GetList()
        {
            return new List<CelestialObject>
            {
                CelestialObjectSeedHelper.GetSun()
            };
        }

        protected override List<Expression<Func<CelestialObject, bool>>> GetBatchExpression()
        {
            return new List<Expression<Func<CelestialObject, bool>>> { x => true };
        }

        protected override List<CelestialObject> SeedList(Expression<Func<CelestialObject, bool>> batchExpression)
        {
            return GetList().Where(batchExpression.Compile()).ToList();
        }

        protected override Func<CelestialObject, string> DuplicateCheckFunction
        {
            get { return x => x.Code; }
        }
    }
}