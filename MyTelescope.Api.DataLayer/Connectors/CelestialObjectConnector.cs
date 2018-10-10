namespace MyTelescope.Api.DataLayer.Connectors
{
    using System;
    using System.Linq.Expressions;
    using Ef.Utilities.Interfaces;
    using Ef.Utilities.Models;
    using Helpers;
    using SolarSystem.Models.CelestialObject;
    using Utilities.Models.Filter;

    public class CelestialObjectConnector : ContextConnector<CelestialObjectModel>
    {
        public CelestialObjectConnector(IContextContainer contextContainer) :
            base(contextContainer)
        {
        }

        protected override Expression<Func<CelestialObjectModel, bool>> GetCustomExpression(FilterModel filter)
        {
            return CustomFilterItemHelper.GetCelestialObjectTypeExpression<CelestialObjectModel>(filter);
        }
    }
}
