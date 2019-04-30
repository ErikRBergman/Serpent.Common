namespace Serpent.Common.Xml.UtilityClasses
{
    using System.Collections.Generic;

    internal class ImmutableHashSet<TType> : ImmutableThing<HashSet<TType>>
    {
        public ImmutableHashSet()
            : base(new HashSet<TType>())
        {
        }

        public void Add(TType item)
        {
            this.Update(item, this.AddToHashSet);
        }

        public bool Contains(TType item)
        {
            return this.Value.Contains(item);
        }

        private HashSet<TType> AddToHashSet(HashSet<TType> hashSet, TType i)
        {
            if (!hashSet.Contains(i))
            {
                var copy = this.GetHashSetCopy();
                copy.Add(i);
                return copy;
            }

            return hashSet;
        }

        private HashSet<TType> GetHashSetCopy()
        {
            return new HashSet<TType>(this.Value);
        }
    }
}