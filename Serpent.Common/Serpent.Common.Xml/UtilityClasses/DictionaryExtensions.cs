namespace Serpent.Common.Xml.UtilityClasses
{
    using System.Collections.Generic;

    // TODO: Move this to base type extensions
    internal static class DictionaryExtensions
    {
        public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> items)
        {
            var dictionary = new Dictionary<TKey, TValue>();

            foreach (var item in items)
            {
                dictionary.Add(item.Key, item.Value);
            }

            return dictionary;
        }
    }
}