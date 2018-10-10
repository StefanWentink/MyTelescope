namespace MyTelescope.Api.DataLayer.Connectors
{
    using System;
    using System.Linq.Expressions;
    using Ef.Utilities.Interfaces;
    using Ef.Utilities.Models;
    using SolarSystem.Models.CelestialObject;
    using Utilities.Models.Filter;

    public class CelestialObjectTypeConnector : ContextConnector<CelestialObjectTypeModel>
    {
        public CelestialObjectTypeConnector(IContextContainer contextContainer) : 
            base(contextContainer)
        {
        }

        protected override Expression<Func<CelestialObjectTypeModel, bool>> GetCustomExpression(FilterModel filter)
        {
            return null;
        }
    }
}
