namespace MyTelescope.Utilities.Helpers.Reflection
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;

    public static class ReflectionHelper
    {
        /// <summary>
        /// Get order selector
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static Expression<Func<TModel, object>> MemberSelector<TModel>(string propertyName)
        {
            var parameter = Expression.Parameter(typeof(TModel), nameof(TModel));
            var property = GetPropertyExpression(parameter, propertyName);

            var conversion = Expression.Convert(property, typeof(object));
            var operand = conversion.Operand;

            if (operand is MemberExpression body)
            {
                var objectConversion = Expression.Convert(body, typeof(object));
                return Expression.Lambda<Func<TModel, object>>(objectConversion, parameter);
            }

            throw new InvalidOperationException($"{property} is not a property of {typeof(TModel)}.");
        }

        /// <summary>
        /// Select value of property
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static Expression<Func<TModel, TValue>> MemberSelector<TModel, TValue>(string propertyName)
        {
            var parameter = Expression.Parameter(typeof(TModel), nameof(TModel));
            
            var property = GetPropertyExpression(parameter, propertyName);
            var operand = Expression.Convert(property, typeof(TValue)).Operand;

            if (operand is MemberExpression body)
            {
                return Expression.Lambda<Func<TModel, TValue>>(body, parameter);
            }

            throw new InvalidOperationException($"{property} is not a property of {typeof(TModel)}.");
        }

        public static bool IsNullable<TModel>(string propertyName)
        {
            var parameter = Expression.Parameter(typeof(TModel), nameof(TModel));
            var property = GetPropertyExpression(parameter, propertyName);
            var type = property.Type;
            var typeInfo = type.GetTypeInfo();

            if (typeInfo.IsClass)
            {
                return true;
            }

            if (typeInfo.IsValueType && type.IsConstructedGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                return true;
            }

            return false;
        }

        public static Type GetPropertyType<TModel>(string propertyName)
        {
            var parameter = Expression.Parameter(typeof(TModel), nameof(TModel));
            var property = GetPropertyExpression(parameter, propertyName);
            return property.Type;
        }

        private static Expression GetPropertyExpression(ParameterExpression parameter, string propertyName)
        {
            return propertyName.Split('.').Aggregate<string, Expression>(parameter, Expression.Property);
        }

        /// <summary>
        /// Check if type is nullable Enum
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsNullableEnum(this Type type)
        {
            var underlyingType = Nullable.GetUnderlyingType(type);
            return underlyingType?.IsEnum ?? false;
        }
    }
}
