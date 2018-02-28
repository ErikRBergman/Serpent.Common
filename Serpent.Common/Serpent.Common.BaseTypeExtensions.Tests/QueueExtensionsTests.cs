namespace Serpent.Common.BaseTypeExtensions.Tests
{
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Serpent.Common.BaseTypeExtensions.Collections;

    [TestClass]
    public class QueueExtensionsTests
    {
        [TestMethod]
        public void ToQueueTest()
        {
            var items = new[] { 1, 2, 3, 4, 5, 6 };

            var queue = items.ToQueue();

            Assert.IsNotNull(queue);

            Assert.AreEqual(items.Length, queue.Count);

            foreach (var item in items)
            {
                var queueItem = queue.Dequeue();
                Assert.AreEqual(item, queueItem);
            }
        }

        [TestMethod]
        public void EnqueueRangeTest()
        {
            var items = new[] { 1, 2, 3, 4, 5, 6 };

            var queue = new Queue<int>();

            queue.EnqueueRange(items);

            Assert.AreEqual(items.Length, queue.Count);

            foreach (var item in items)
            {
                var queueItem = queue.Dequeue();
                Assert.AreEqual(item, queueItem);
            }
        }

        [TestMethod]
        public void DequeueManyTest()
        {
            var items = new[] { 1, 2, 3, 4, 5, 6 };

            var queue = new Queue<int>();

            queue.EnqueueRange(items);

            var dequeued = queue.DequeueMany(3).ToArray();

            Assert.AreEqual(3, dequeued.Length);

            Assert.AreEqual(1, dequeued[0]);
            Assert.AreEqual(2, dequeued[1]);
            Assert.AreEqual(3, dequeued[2]);

            dequeued = queue.DequeueMany(6).ToArray();
            Assert.AreEqual(3, dequeued.Length);

            Assert.AreEqual(0, queue.Count);

            Assert.AreEqual(4, dequeued[0]);
            Assert.AreEqual(5, dequeued[1]);
            Assert.AreEqual(6, dequeued[2]);
        }
    }
}