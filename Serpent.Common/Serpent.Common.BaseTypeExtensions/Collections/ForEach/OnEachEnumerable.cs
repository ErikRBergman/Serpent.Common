namespace Serpent.Common.BaseTypeExtensions.Collections.ForEach
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    internal struct OnEachEnumerable<T> : IEnumerable<T>
    {
        private readonly IEnumerable<T> inner;

        private readonly Action<T> action;

        public OnEachEnumerable(IEnumerable<T> inner, Action<T> action)
        {
            this.inner = inner;
            this.action = action;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var enumerator = this.inner.GetEnumerator();
            return new OnEachEnumerator<T>(enumerator, this.action);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}