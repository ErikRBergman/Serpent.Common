namespace Serpent.Common.BaseTypeExtensions.Collections
{
    using System;
    using System.Collections.Generic;

    public static class DictionaryExtensions
    {
        public static TValue GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue @default = default(TValue))
        {
            return dictionary.TryGetValue(key, out TValue value) ? value : @default;
        }

        public static IDictionary<TKey, TValue> AddRange<TKey, TValue, TOther>(this IDictionary<TKey, TValue> dictionary, IEnumerable<TOther> items, Func<TOther, TKey> keySelector, Func<TOther, TValue> valueSelector)
        {
            foreach (var item in items)
            {
                dictionary.Add(keySelector(item), valueSelector(item));
            }

            return dictionary;
        }

        public static Dictionary<TKey, TValue> AddRange<TKey, TValue, TOther>(this Dictionary<TKey, TValue> dictionary, IEnumerable<TOther> items, Func<TOther, TKey> keySelector, Func<TOther, TValue> valueSelector)
        {
            foreach (var item in items)
            {
                dictionary.Add(keySelector(item), valueSelector(item));
            }

            return dictionary;
        }

        public static IDictionary<TKey, TValue> SetRange<TKey, TValue, TOther>(this IDictionary<TKey, TValue> dictionary, IEnumerable<TOther> items, Func<TOther, TKey> keySelector, Func<TOther, TValue> valueSelector)
        {
            foreach (var item in items)
            {
                dictionary[keySelector(item)] = valueSelector(item);
            }

            return dictionary;
        }

        public static Dictionary<TKey, TValue> SetRange<TKey, TValue, TOther>(this Dictionary<TKey, TValue> dictionary, IEnumerable<TOther> items, Func<TOther, TKey> keySelector, Func<TOther, TValue> valueSelector)
        {
            foreach (var item in items)
            {
                dictionary[keySelector(item)] = valueSelector(item);
            }

            return dictionary;
        }

        public static IDictionary<TKey, TValue> RemoveRange<TKey, TValue, TOther>(this IDictionary<TKey, TValue> dictionary, IEnumerable<TOther> items, Func<TOther, TKey> keySelector)
        {
            foreach (var item in items)
            {
                dictionary.Remove(keySelector(item));
            }

            return dictionary;
        }

        public static Dictionary<TKey, TValue> RemoveRange<TKey, TValue, TOther>(this Dictionary<TKey, TValue> dictionary, IEnumerable<TOther> items, Func<TOther, TKey> keySelector)
        {
            foreach (var item in items)
            {
                dictionary.Remove(keySelector(item));
            }

            return dictionary;
        }


        public static IDictionary<TKey, TValue> RemoveRange<TKey, TValue, TOther>(this IDictionary<TKey, TValue> dictionary, IEnumerable<TKey> keys)
        {
            foreach (var key in keys)
            {
                dictionary.Remove(key);
            }

            return dictionary;
        }

        public static Dictionary<TKey, TValue> RemoveRange<TKey, TValue, TOther>(this Dictionary<TKey, TValue> dictionary, IEnumerable<TKey> keys)
        {
            foreach (var key in keys)
            {
                dictionary.Remove(key);
            }

            return dictionary;
        }

        public static IDictionary<TKey, TValue> AddF<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue value)
        {
            dictionary.Add(key, value);
            return dictionary;
        }

        public static Dictionary<TKey, TValue> AddF<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, TValue value)
        {
            dictionary.Add(key, value);
            return dictionary;
        }

        public static IDictionary<TKey, TValue> AddIfMissing<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue value)
        {
            if (!dictionary.ContainsKey(key))
            {
                dictionary.Add(key, value);
            }

            return dictionary;
        }

        public static Dictionary<TKey, TValue> AddIfMissing<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, TValue value)
        {
            if (!dictionary.ContainsKey(key))
            {
                dictionary.Add(key, value);
            }

            return dictionary;
        }

        public static bool DoForKey<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, Action<TKey, TValue> action)
        {
            if (dictionary.TryGetValue(key, out var value))
            {
                action(key, value);
                return true;
            }

            return false;
        }

        public static bool DoForKey<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, Action<TValue> action)
        {
            if (dictionary.TryGetValue(key, out var value))
            {
                action(value);
                return true;
            }

            return false;
        }

        public static bool DoForKey<TKey, TValue, TContext>(this IDictionary<TKey, TValue> dictionary, TKey key, TContext context, Action<TKey, TValue, TContext> action)
        {
            if (dictionary.TryGetValue(key, out var value))
            {
                action(key, value, context);
                return true;
            }

            return false;
        }

        public static bool DoForKey<TKey, TValue, TContext>(this IDictionary<TKey, TValue> dictionary, TKey key, TContext context, Action<TValue, TContext> action)
        {
            if (dictionary.TryGetValue(key, out var value))
            {
                action(value, context);
                return true;
            }

            return false;
        }

        public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue, TType>(
            this IReadOnlyCollection<TType> items,
            Func<TType, TKey> keySelector,
            Func<TType, TValue> valueSelector)
        {
            var dictionary = new Dictionary<TKey, TValue>(items.Count);

            foreach (var item in items)
            {
                dictionary.Add(keySelector(item), valueSelector(item));
            }

            return dictionary;
        }

        public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue, TType>(
            this IReadOnlyCollection<TType> items,
            Func<TType, TKey> keySelector,
            Func<TType, TValue> valueSelector,
            IEqualityComparer<TKey> equalityComparer)
        {
            var dictionary = new Dictionary<TKey, TValue>(items.Count, equalityComparer);

            foreach (var item in items)
            {
                dictionary.Add(keySelector(item), valueSelector(item));
            }

            return dictionary;
        }
    }
}