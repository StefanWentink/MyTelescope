namespace MyTelescope.Seeder.Helpers
{
    using SolarSystem.Helpers.Seeder;
    using SolarSystem.Models.CelestialObject;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Utilities.Interfaces.Connector;

    public class CelestialObjectTypeSeeder : BaseSeeder<CelestialObjectType, string>
    {
        public CelestialObjectTypeSeeder(IContextConnector<CelestialObjectType> connector) : base(connector)
        {
        }

        private static List<CelestialObjectType> List { get; } = CelestialObjectTypeSeedHelper.GetCelestialObjectTypes();

        protected override List<Expression<Func<CelestialObjectType, bool>>> GetBatchExpression()
        {
            return new List<Expression<Func<CelestialObjectType, bool>>> { x => true };
        }

        protected override List<CelestialObjectType> SeedList(Expression<Func<CelestialObjectType, bool>> batchExpression)
        {
            return List.Where(batchExpression.Compile()).ToList();
        }

        protected override Func<CelestialObjectType, string> DuplicateCheckFunction
        {
            get { return x => x.Code; }
        }
    }
}