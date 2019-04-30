namespace Serpent.Common.Xml.UtilityClasses
{
    using System.Collections.Generic;

    internal class ImmutableDictionaryLight<TKey, TValue> : ImmutableThing<IReadOnlyDictionary<TKey, TValue>>
    {
        public ImmutableDictionaryLight(IReadOnlyDictionary<TKey, TValue> dictionary = null)
            : base(dictionary)
        {
        }

        public ImmutableDictionaryLight(IEnumerable<KeyValuePair<TKey, TValue>> keyValuePairs)
            : base(CopyDictionary(keyValuePairs))
        {
        }

        public void TryAdd(TKey key, TValue value)
        {
            this.Update(key, value, this.AddToDictionary);
        }

        private static Dictionary<TKey, TValue> CopyDictionary(IEnumerable<KeyValuePair<TKey, TValue>> source)
        {
            var newDictionary = new Dictionary<TKey, TValue>();

            if (source != null)
            {
                foreach (var keyValuePair in source)
                {
                    newDictionary.Add(keyValuePair.Key, keyValuePair.Value);
                }
            }

            return newDictionary;
        }

        private IReadOnlyDictionary<TKey, TValue> AddToDictionary(IReadOnlyDictionary<TKey, TValue> currentDictionary, TKey k, TValue v)
        {
            if (!currentDictionary.ContainsKey(k))
            {
                var newDictionary = this.GetDictionaryCopy();
                newDictionary.Add(k, v);
                return newDictionary;
            }

            // no change
            return currentDictionary;
        }

        private Dictionary<TKey, TValue> GetDictionaryCopy()
        {
            IEnumerable<KeyValuePair<TKey, TValue>> source = this.Value;
            return CopyDictionary(source);
        }
    }
}