using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Serpent.Common.BaseTypeExtensions.Tests
{
    using System.Linq;

    using Serpent.Common.BaseTypeExtensions.Collections;

    [TestClass]
    public class ForEachEnumerableTests
    {
        private class MyData
        {
            public MyData(int value)
            {
                this.Value = value;
            }

            public int Value { get; set; }

        }

        [TestMethod]
        public void TestForEach()
        {
            var items = new[] { new MyData(1), new MyData(2), new MyData(3), new MyData(4), new MyData(5), new MyData(6) };

            items.ForEach(i => i.Value++);

            Assert.AreEqual(2, items[0].Value);
            Assert.AreEqual(3, items[1].Value);
            Assert.AreEqual(4, items[2].Value);
            Assert.AreEqual(5, items[3].Value);
            Assert.AreEqual(6, items[4].Value);
            Assert.AreEqual(7, items[5].Value);

            var firstItem = items.Take(1).OnEach(i => i.Value++).FirstOrDefault();

            Assert.AreEqual(3, items[0].Value);
            Assert.AreEqual(3, items[1].Value);
            Assert.AreEqual(4, items[2].Value);
            Assert.AreEqual(5, items[3].Value);
            Assert.AreEqual(6, items[4].Value);
            Assert.AreEqual(7, items[5].Value);

            var items2_3 = items.Skip(2).OnEach(i => i.Value++).Take(2).ToArray();

            Assert.AreEqual(3, items[0].Value);
            Assert.AreEqual(3, items[1].Value);
            Assert.AreEqual(5, items[2].Value);
            Assert.AreEqual(6, items[3].Value);
            Assert.AreEqual(6, items[4].Value);
            Assert.AreEqual(7, items[5].Value);
        }
    }
}
