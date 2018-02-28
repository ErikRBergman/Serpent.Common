namespace Serpent.Common.BaseTypeExtensions.Collections
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class EnumerableExtensions
    {
        /// <summary>
        /// Creates a dictionary from a collection, ignoring duplicate keys
        /// </summary>
        /// <typeparam name="TKey">The key type</typeparam>
        /// <typeparam name="TValue">The collection item type</typeparam>
        /// <param name="collection">The collection</param>
        /// <param name="keySelector">The key selector</param>
        /// <param name="equalityComparer">An equality comparer</param>
        /// <returns>The new dictionary</returns>
        public static Dictionary<TKey, TValue> ToDictionarySafe<TKey, TValue>(this IEnumerable<TValue> collection, Func<TValue, TKey> keySelector, IEqualityComparer<TKey> equalityComparer = null)
        {
            var dictionary = new Dictionary<TKey, TValue>(equalityComparer ?? EqualityComparer<TKey>.Default);

            foreach (var item in collection)
            {
                var key = keySelector(item);

                if (!dictionary.ContainsKey(key))
                {
                    dictionary.Add(key, item);
                }
            }

            return dictionary;
        }

        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> sourceItems, Func<TSource, TKey> keySelector)
        {
            var keysSeen = new HashSet<TKey>();

            foreach (var item in sourceItems)
            {
                if (keysSeen.Add(keySelector(item)))
                {
                    yield return item;
                }
            }
        }

        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> sourceItems, Func<TSource, TKey> keySelector, IEqualityComparer<TKey> equalityComparer)
        {
            var keysSeen = new HashSet<TKey>(equalityComparer);

            foreach (var item in sourceItems)
            {
                if (keysSeen.Add(keySelector(item)))
                {
                    yield return item;
                }
            }
        }

        public static IReadOnlyCollection<T> ToReadOnlyCollection<T>(this IEnumerable<T> sourceItems)
        {
            return sourceItems.ToArray();
        }

        public static HashSet<T> ToHashSet<T>(this IEnumerable<T> sourceItems)
        {
            return new HashSet<T>(sourceItems);
        }

        public static HashSet<T> ToHashSet<T>(this IEnumerable<T> sourceItems, IEqualityComparer<T> equalityComparer)
        {
            return new HashSet<T>(sourceItems, equalityComparer);
        }

        public static Queue<T> ToQueue<T>(this IEnumerable<T> items)
        {
            return new Queue<T>(items);
        }

        public static void ForEach<T>(this IEnumerable<T> items, Action<T> action)
        {
            foreach (var item in items)
            {
                action(item);
            }
        }
    }
}