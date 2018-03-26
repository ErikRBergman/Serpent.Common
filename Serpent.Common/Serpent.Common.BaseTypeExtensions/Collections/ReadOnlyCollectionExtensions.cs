namespace Serpent.Common.BaseTypeExtensions.Collections
{
    using System;
    using System.Collections.Generic;

    public static class ReadOnlyCollectionExtensions
    {
        public static IReadOnlyCollection<T> ForEach<T>(this IReadOnlyCollection<T> items, Action<T> action)
        {
            foreach (var item in items)
            {
                action(item);
            }

            return items;
        }
    }
}