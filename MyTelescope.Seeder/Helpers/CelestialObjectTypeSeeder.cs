namespace MyTelescope.Seeder.Helpers
{
    using SolarSystem.Helpers.Seeder;
    using SolarSystem.Models.CelestialObject;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Utilities.Interfaces.Connector;

    public class CelestialObjectTypeSeeder : BaseSeeder<CelestialObjectTypeModel, string>
    {
        public CelestialObjectTypeSeeder(IContextConnector<CelestialObjectTypeModel> connector) : base(connector)
        {
        }

        private static List<CelestialObjectTypeModel> List { get; } = CelestialObjectTypeSeedHelper.GetCelestialObjectTypes();

        protected override List<Expression<Func<CelestialObjectTypeModel, bool>>> GetBatchExpression()
        {
            return new List<Expression<Func<CelestialObjectTypeModel, bool>>> { x => true };
        }

        protected override List<CelestialObjectTypeModel> SeedList(Expression<Func<CelestialObjectTypeModel, bool>> batchExpression)
        {
            return List.Where(batchExpression.Compile()).ToList();
        }

        protected override Func<CelestialObjectTypeModel, string> DuplicateCheckFunction
        {
            get { return x => x.Code; }
        }
    }
}