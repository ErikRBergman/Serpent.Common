using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serpent.Common.BaseTypeExtensions.Collections
{
    public static class QueueExtensions
    {
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

        public static IEnumerable<T> DequeueMany<T>(this Queue<T> queue, int count)
        {
            var result = new List<T>(Math.Min(count, queue.Count));

            for (int i = 0; i < count; i++)
            {
                if (queue.Count > 0)
                {
                    result.Add(queue.Dequeue());
                }
            }

            return result;
        }
    }
}
