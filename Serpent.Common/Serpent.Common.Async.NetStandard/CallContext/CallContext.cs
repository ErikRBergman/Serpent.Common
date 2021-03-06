﻿// ReSharper disable once CheckNamespace
namespace Serpent.Common.Async
{
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using System.Threading;

    public class CallContext
    {
        private readonly AsyncLocal<ImmutableDictionary<string, object>> _executionTreeStorage = new AsyncLocal<ImmutableDictionary<string, object>>();

        public static CallContext Current { get; } = new CallContext();

        public CallContext RemoveItem(string key)
        {
            var dictionary = this._executionTreeStorage.Value;

            if (dictionary != null)
            {
                this._executionTreeStorage.Value = dictionary.Remove(key);
            }

            return this;
        }

        public CallContext SetItem(string key, object value)
        {
            this._executionTreeStorage.Value = (this._executionTreeStorage.Value ?? ImmutableDictionary<string, object>.Empty).SetItem(key, value);
            return this;
        }

        public CallContext SetItems(IEnumerable<KeyValuePair<string, object>> items)
        {
            this._executionTreeStorage.Value = (this._executionTreeStorage.Value ?? ImmutableDictionary<string, object>.Empty).SetItems(items);
            return this;
        }

        public object GetItem(string key)
        {
            var dictionary = this._executionTreeStorage.Value;

            if (dictionary == null || !dictionary.TryGetValue(key, out var value))
            {
                return null;
            }

            return value;
        }
    }
}
