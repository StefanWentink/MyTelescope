﻿namespace MyTelescope.Utilities.Models.Reflection
{
    using System.Collections.Generic;
    using System.Linq.Expressions;

    public class ParameterRebinder : ExpressionVisitor
    {
        private Dictionary<ParameterExpression, ParameterExpression> Map { get; }

        public ParameterRebinder(Dictionary<ParameterExpression, ParameterExpression> map)
        {
            Map = map ?? new Dictionary<ParameterExpression, ParameterExpression>();
        }

        public static Expression ReplaceParameters(Dictionary<ParameterExpression, ParameterExpression> map, Expression exp)
        {
            return new ParameterRebinder(map).Visit(exp);
        }

        protected override Expression VisitParameter(ParameterExpression p)
        {
            if (Map.TryGetValue(p, out var replacement))
            {
                p = replacement;
            }

            return base.VisitParameter(p);
        }
    }
}
