namespace MyTelescope.Utilities.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
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
            return value == null || !value.Any();
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
            return value == null || !value.Any(expression);
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


        /// <summary>
        /// Calculates available take
        /// </summary>
        /// <param name="skip">skip number</param>
        /// <param name="take">requested take, 0 will be altered to match maximum available records </param>
        /// <param name="recordCount">number of records, negative will not be evaluated</param>
        /// <returns> calculated take</returns>
        public static int CalculateTakeByRecordCount(int skip, int take, int recordCount)
        {
            return CalculateTake(skip, take, recordCount, 0);
        }

        /// <summary>
        /// Calculates available take
        /// </summary>
        /// <param name="skip">skip number</param>
        /// <param name="take">requested take, 0 will be altered to match maximum available records </param>
        /// <param name="maxTake">maximum allowed take, 0 or negative will not be evaluated</param>
        /// <returns> calculated take</returns>
        public static int CalculateTakeByMaxTake(int skip, int take, int maxTake)
        {
            return CalculateTake(skip, take, -1, maxTake);
        }


        /// <summary>
        /// Calculates available and/or allowed take
        /// </summary>
        /// <param name="skip">skip number</param>
        /// <param name="take">requested take, 0 will be altered to match maxtake or maximum available records </param>
        /// <param name="recordCount">number of records, negative will not be evaluated</param>
        /// <param name="maxTake"></param>
        /// <returns> calculated take </returns>
        public static int CalculateTake(int skip, int take, int recordCount, int maxTake)
        {
            if (take < 0)
            {
                throw new ArgumentException($"{take} is an invalid value for {nameof(take)}.", nameof(take));
            }

            if (recordCount == 0)
            {
                return 0;
            }

            var resultTake = take;

            if ((maxTake > 0 && resultTake > maxTake) || resultTake == 0)
            {
                resultTake = maxTake;
            }

            if (recordCount > 0 && skip + resultTake > recordCount)
            {
                resultTake = Math.Max(recordCount - skip, 0);
            }

            return resultTake;
        }

        /// <summary>
        /// Adds or updates an item to a list based on finding the item by an expression.
        /// Only the first match
        /// Replaces only by one item
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="model"></param>
        /// <param name="function"></param>
        public static void AddOrUpdateItem<T>(this List<T> list, T model, Func<T, bool> function)
        {
            list.AddOrUpdateItem(model, function, true, true);
        }

        /// <summary>
        /// Adds or updates an item to a list based on finding the item by an expression.
        /// Only the first match
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="model"></param>
        /// <param name="function"></param>
        /// <param name="firstMatchOnly">Remove all items </param>
        public static void AddOrUpdateItem<T>(this List<T> list, T model, Func<T, bool> function, bool firstMatchOnly)
        {
            list.AddOrUpdateItem(model, function, firstMatchOnly, true);
        }


        /// <summary>
        /// Adds or updates an item to a list based on finding the item by an expression.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="model"></param>
        /// <param name="function"></param>
        /// <param name="firstMatchOnly">Remove all items </param>
        /// <param name="replaceOnce">Replace possible multiple deletions only the first time.</param>
        public static void AddOrUpdateItem<T>(this List<T> list, T model, Func<T, bool> function, bool firstMatchOnly, bool replaceOnce)
        {
            var replacements = 0;
            var index = list.FindIndex(new Predicate<T>(function));
            while (index >= 0)
            {
                list.RemoveAt(index);

                if (replacements < 1 || !replaceOnce)
                {
                    list.Insert(index, model);
                    replacements += 1;
                }

                index = firstMatchOnly ? -1 : list.FindIndex(index, new Predicate<T>(function));
            }

            // Actual insert
            if (replacements < 1)
            {
                list.Add(model);
            }
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
