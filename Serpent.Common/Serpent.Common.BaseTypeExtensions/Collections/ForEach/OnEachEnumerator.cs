namespace Serpent.Common.BaseTypeExtensions.Collections.ForEach
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    internal struct OnEachEnumerator<T> : IEnumerator<T>
    {
        private readonly IEnumerator<T> enumerator;

        private readonly Action<T> action;

        public OnEachEnumerator(IEnumerator<T> enumerator, Action<T> action)
        {
            this.enumerator = enumerator;
            this.action = action;
        }

        public void Dispose()
        {
            this.enumerator.Dispose();
        }

        public bool MoveNext()
        {
            var result = this.enumerator.MoveNext();

            if (result)
            {
                this.action(this.Current);
            }

            return result;
        }

        public void Reset()
        {
            this.enumerator.Reset();
        }

        public T Current => this.enumerator.Current;

        object IEnumerator.Current => this.Current;
    }
}