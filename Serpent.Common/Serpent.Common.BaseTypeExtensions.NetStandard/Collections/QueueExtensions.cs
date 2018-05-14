namespace Serpent.Common.BaseTypeExtensions.Collections
{
    using System;
    using System.Collections.Generic;

    public static class QueueExtensions
    {
        public static IEnumerable<T> DequeueMany<T>(this Queue<T> queue, int count)
        {
            var result = new List<T>(Math.Min(count, queue.Count));

            for (var i = 0; i < count; i++)
            {
                if (queue.Count > 0)
                {
                    result.Add(queue.Dequeue());
                }
            }

            return result;
        }

        public static Queue<T> EnqueueRange<T>(this Queue<T> queue, IEnumerable<T> collection)
        {
            foreach (var item in collection)
            {
                queue.Enqueue(item);
            }

            return queue;
        }

        public static Queue<T> ToQueue<T>(IEnumerable<T> collection)
        {
            return new Queue<T>(collection);
        }
    }
}