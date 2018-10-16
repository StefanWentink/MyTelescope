namespace MyTelescope.Api.DataLayer.Connectors
{
    using Ef.Utilities.Interfaces;
    using Ef.Utilities.Models;
    using SolarSystem.Models.CelestialObject;
    using System;
    using System.Linq.Expressions;
    using Utilities.Models.Filter;

    public class CelestialObjectPositionConnector : ContextConnector<CelestialObjectPosition>
    {
        public CelestialObjectPositionConnector(IContextContainer contextContainer) :
            base(contextContainer)
        {
        }

        protected override Expression<Func<CelestialObjectPosition, bool>> GetCustomExpression(FilterModel filter)
        {
            return null;
        }
    }
}