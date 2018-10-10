namespace MyTelescope.Seeder.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using SolarSystem.Helpers.Seeder;
    using SolarSystem.Models.CelestialObject;
    using Utilities.Interfaces.Connector;

    public class CelestialObjectSunSeeder : BaseSeeder<CelestialObjectModel, string>
    {
        public CelestialObjectSunSeeder(
            IContextConnector<CelestialObjectModel> connector)
            : base(connector)
        {
        }

        private static List<CelestialObjectModel> GetList()
        {
            return new List<CelestialObjectModel>
            {
                CelestialObjectSeedHelper.GetSun()
            };
        }

        protected override List<Expression<Func<CelestialObjectModel, bool>>> GetBatchExpression()
        {
            return new List<Expression<Func<CelestialObjectModel, bool>>> { x => true };
        }

        protected override List<CelestialObjectModel> SeedList(Expression<Func<CelestialObjectModel, bool>> batchExpression)
        {
            return GetList().Where(batchExpression.Compile()).ToList();
        }

        protected override Func<CelestialObjectModel, string> DuplicateCheckFunction
        {
            get { return x => x.Code; }
        }
    }
}