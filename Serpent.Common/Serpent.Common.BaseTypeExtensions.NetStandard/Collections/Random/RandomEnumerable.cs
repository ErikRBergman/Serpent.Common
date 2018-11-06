using System;
using System.Collections.Generic;
using System.Text;

namespace Serpent.Common.BaseTypeExtensions.Collections.Random
{
    using System.Collections;
    using System.Linq;

    using Random = System.Random;

    public class RandomEnumerable<T> : IEnumerable<T>
    {
        private readonly T[] collection;

        public RandomEnumerable(IEnumerable<T> collection, int shuffleCountPerItem = 3)
        {
            var collectionArray = collection.ToArray();

            var random = new Random();

            var length = collectionArray.Length;
            var iterations = length * shuffleCountPerItem;

            for (int i = 0; i < iterations; i++)
            {
                collectionArray.Swap(random.Next(0, length), random.Next(0, length));
            }

            this.collection = collectionArray;
        }

        public IEnumerator<T> GetEnumerator()
        {
            IReadOnlyCollection<T> roc = this.collection;
            return roc.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
