namespace MyTelescope.Utilities.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text;

    public static class CollectionHelper
    {
        /// <summary>
        /// Checks wether the collection is null or contains no elements
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> value)
        {
            return value?.Any() != true;
        }

        /// <summary>
        /// Checks wether the collection is null or contains no elements respecting the given expression
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty<T>(this IList<T> value, Func<T, bool> expression)
        {
            return value?.Any(expression) != true;
        }

        /// <summary>
        /// Parses a list even if value = null
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static List<T> ParseList<T>(this IEnumerable<T> value)
        {
            return value?.ToList() ?? new List<T>();
        }

        /// <summary>
        /// Fetches all elements with are default for current type (default(T))
        /// Returns an empty Collection in case of null reference
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static List<T> ValuesOnly<T>(this IEnumerable<T> value)
            where T : IComparable
        {
            if (value == null)
            {
                return new List<T>();
            }

            return value.Where(x => !Equals(x, default(T))).ToList();
        }

        /// <summary>
        /// Cast list to more generic type
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TInterface"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static List<TModel> ToList<TModel, TInterface>(this IEnumerable<TInterface> value)
            where TModel : class, TInterface
        {
            return value.ToList<TModel, TInterface>(true);
        }

        /// <summary>
        /// Combines to Lists for direct result, use in constructor etc.
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="value"></param>
        /// <param name="addRange"></param>
        /// <returns></returns>
        public static IEnumerable<TModel> Combine<TModel>(this IEnumerable<TModel> value, IEnumerable<TModel> addRange)
        {
            var result = value.ToList();
            result.AddRange(addRange);
            return result;
        }

        /// <summary>
        /// Cast list to more generic type
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TInterface"></typeparam>
        /// <param name="value"></param>
        /// <param name="swallowInvalidCasts"></param>
        /// <returns></returns>
        public static List<TModel> ToList<TModel, TInterface>(this IEnumerable<TInterface> value, bool swallowInvalidCasts)
            where TModel : class, TInterface
        {
            if (!swallowInvalidCasts)
            {
                return value.Select(x => (TModel)x).ToList();
            }

            var result = new List<TModel>();
            foreach (var t in value)
            {
                try
                {
                    result.Add((TModel)t);
                }
                catch (InvalidCastException)
                {
                    // swallow exception
                }
            }

            return result;
        }

        public static bool IsEnumerableObject(object value)
        {
            return value is IEnumerable<object>;
        }

        /// <summary>
        /// Converts an object (containing a IEnumerable) to a list of the selected value
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="value"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static List<TValue> EnumerableObjectToList<TValue>(object value, Func<object, TValue> expression)
        {
            if (value != null && value is IEnumerable<object> objectArray)
            {
                return objectArray.Select(expression).ToList();
            }

            return new List<TValue>();
        }

        public static Expression<Func<TValue, bool>> GetCollectionFilterExpression<TValue>(List<TValue> filterValue)
        {
            return x => filterValue.Contains(x);
        }

        public static bool In<T>(this T val, params T[] values)
            where T : struct
        {
            return values.Contains(val);
        }

        public static string ConcatString(this IEnumerable<object> values, string concat)
        {
            var result = new StringBuilder();

            foreach (var value in values)
            {
                if (result.Length > 0)
                {
                    result.Append(concat);
                }

                result.Append(value);
            }

            return result.ToString();
        }
    }
}