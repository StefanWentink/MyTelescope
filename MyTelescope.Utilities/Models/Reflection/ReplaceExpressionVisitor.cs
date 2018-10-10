namespace MyTelescope.Utilities.Models.Reflection
{
    using System.Linq.Expressions;

    public class ReplaceExpressionVisitor : ExpressionVisitor
    {
        private Expression OldValueHolder { get; }

        private Expression NewValueHolder { get; }

        public ReplaceExpressionVisitor(Expression oldValue, Expression newValue)
        {
            OldValueHolder = oldValue;
            NewValueHolder = newValue;
        }

        public override Expression Visit(Expression node)
        {
            return node == OldValueHolder ? NewValueHolder : base.Visit(node);
        }
    }
}
