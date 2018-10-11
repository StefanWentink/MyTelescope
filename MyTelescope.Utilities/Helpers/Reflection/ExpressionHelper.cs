namespace MyTelescope.Utilities.Helpers.Reflection
{
    using Models.Reflection;
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    public static class ExpressionHelper
    {
        public static Expression<T> Compose<T>(this Expression<T> first, Expression<T> second, Func<Expression, Expression, Expression> merge)
        {
            // build parameter map (from parameters of second to parameters of first)
            var map = first.Parameters.Select((f, i) => new { f, s = second.Parameters[i] }).ToDictionary(p => p.s, p => p.f);

            // replace parameters in the second lambda expression with parameters from the first
            var secondBody = ParameterRebinder.ReplaceParameters(map, second.Body);

            // apply composition of lambda expression bodies to parameters from the first expression
            return Expression.Lambda<T>(merge(first.Body, secondBody), first.Parameters);
        }

        public static Expression<Func<TR, bool>> ComposeSub<T, TR>(this Expression<Func<T, bool>> expr1, Expression<Func<TR, bool>> expr2, bool and)
            where TR : T
        {
            var parameter = Expression.Parameter(typeof(TR));

            var leftVisitor = new ReplaceExpressionVisitor(expr1.Parameters[0], parameter);
            var left = leftVisitor.Visit(expr1.Body);

            var secondaryExpression = expr2;
            var expressionOperatorAnd = and;

            if (secondaryExpression == null)
            {
                secondaryExpression = x => true;
                expressionOperatorAnd = true;
            }

            var rightVisitor = new ReplaceExpressionVisitor(secondaryExpression.Parameters[0], parameter);
            var right = rightVisitor.Visit(secondaryExpression.Body);

            return Expression.Lambda<Func<TR, bool>>(
                expressionOperatorAnd ?
                Expression.AndAlso(left, right) :
                Expression.OrElse(left, right), parameter);
        }

        public static Expression<Func<TR, bool>> CombineSubExpressionOr<T, TR>(this Expression<Func<T, bool>> expressionbase, Expression<Func<TR, bool>> expressionaddition)
            where TR : T
        {
            return expressionbase.CombineSubExpression(expressionaddition, true);
        }

        public static Expression<Func<TR, bool>> CombineSubExpressionAnd<T, TR>(this Expression<Func<T, bool>> expressionbase, Expression<Func<TR, bool>> expressionaddition)
            where TR : T
        {
            return expressionbase.CombineSubExpression(expressionaddition, false);
        }

        public static Expression<Func<T, bool>> CombineExpressionOr<T>(this Expression<Func<T, bool>> expressionbase, Expression<Func<T, bool>> expressionaddition)
        {
            return expressionbase.CombineExpression(expressionaddition, true);
        }

        public static Expression<Func<TR, bool>> CastSubExpression<T, TR>(this Expression<Func<T, bool>> expressionbase)
            where TR : T
        {
            return expressionbase.CombineSubExpressionOr<T, TR>(null);
        }

        public static Expression<Func<T, bool>> CombineExpressionAnd<T>(this Expression<Func<T, bool>> expressionbase, Expression<Func<T, bool>> expressionaddition)
        {
            return expressionbase.CombineExpression(expressionaddition, false);
        }

        private static Expression<Func<T, bool>> CombineExpression<T>(this Expression<Func<T, bool>> expressionbase, Expression<Func<T, bool>> expressionaddition, bool or)
        {
            if (expressionbase == null)
            {
                return expressionaddition;
            }

            if (expressionaddition == null)
            {
                return expressionbase;
            }

            return or ? expressionbase.Or(expressionaddition) : expressionbase.And(expressionaddition);
        }

        private static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
        {
            return first.Compose(second, Expression.AndAlso);
        }

        private static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
        {
            return first.Compose(second, Expression.OrElse);
        }

        private static Expression<Func<TR, bool>> CombineSubExpression<T, TR>(this Expression<Func<T, bool>> expressionbase, Expression<Func<TR, bool>> expressionaddition, bool or)
            where TR : T
        {
            if (expressionbase == null)
            {
                return expressionaddition;
            }

            return or ? expressionbase.OrSub(expressionaddition) : expressionbase.AndSub(expressionaddition);
        }

        private static Expression<Func<TR, bool>> AndSub<T, TR>(this Expression<Func<T, bool>> first, Expression<Func<TR, bool>> second)
            where
            TR : T
        {
            return ComposeSub(first, second, true);
        }

        private static Expression<Func<TR, bool>> OrSub<T, TR>(this Expression<Func<T, bool>> first, Expression<Func<TR, bool>> second)
            where TR : T
        {
            return first.ComposeSub(second, false);
        }

        /// <summary>
        /// Applies a type based property-selector onto to a model based property-selector
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TSelector"></typeparam>
        /// <param name="propertySelector"></param>
        /// <param name="propertyPredicate"></param>
        /// <returns></returns>
        public static Expression<Func<TModel, bool>> CombineSelectorParamExpression<TModel, TSelector>(
            this Expression<Func<TModel, TSelector>> propertySelector,
            Expression<Func<TSelector, bool>> propertyPredicate)
        {
            if (!(propertySelector.Body is MemberExpression memberExpression))
            {
                throw new ArgumentException("propertySelector");
            }

            var expr = Expression.Lambda<Func<TModel, bool>>(propertyPredicate.Body, propertySelector.Parameters);
            var rebinder = new ParamExpressionToMemberExpressionRebinder(propertyPredicate.Parameters[0], memberExpression);
            return (Expression<Func<TModel, bool>>)rebinder.Visit(expr);
        }
    }
}