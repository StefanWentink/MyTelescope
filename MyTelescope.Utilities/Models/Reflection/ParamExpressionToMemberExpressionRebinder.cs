namespace MyTelescope.Utilities.Models.Reflection
{
    using System.Linq.Expressions;

    public class ParamExpressionToMemberExpressionRebinder : ExpressionVisitor
    {
        private ParameterExpression ParamExpression { get; }

        private Expression MemberExpression { get; }

        public ParamExpressionToMemberExpressionRebinder(ParameterExpression paramExpression, Expression memberExpression)
        {
            ParamExpression = paramExpression;
            MemberExpression = memberExpression;
        }

        /// <summary>
        /// Param to member rebinder
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        public override Expression Visit(Expression node)
        {
            return base.Visit(node == null || node == ParamExpression ? MemberExpression : node);
        }
    }
}
