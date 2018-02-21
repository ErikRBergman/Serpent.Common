namespace Serpent.Common.BaseTypeExtensions.Tests
{
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Serpent.Common.BaseTypeExtensions.Collections;

    [TestClass]
    public class DictionaryExtensionsTests
    {
        [TestMethod]
        public void GetValueOrDefaultTest()
        {
            var dictionary = new[] { new Item(1, "One"), new Item(2, "Two"), new Item(3, "Three") }.ToDictionary(i => i.Key);

            Assert.IsNotNull(dictionary.GetValueOrDefault(1));
            Assert.IsNull(dictionary.GetValueOrDefault(0));

            var item = dictionary.GetValueOrDefault(2);

            Assert.IsNotNull(item);

            Assert.IsTrue(item.Key == 2 && item.Value == "Two");
        }

        [TestMethod]
        public void ToDictionaryTests()
        {
            var dictionary = new[] { new Item(1, "One"), new Item(2, "Two"), new Item(3, "Three") }.ToDictionary(i => i.Key, i => i.Value);

            Assert.IsNotNull(dictionary.GetValueOrDefault(1));
            Assert.IsNull(dictionary.GetValueOrDefault(0));

            var value = dictionary.GetValueOrDefault(2);

            Assert.IsNotNull(value);

            Assert.AreEqual("Two", value);
        }

        private class Item
        {
            public Item(int key, string value)
            {
                this.Key = key;
                this.Value = value;
            }

            public int Key { get; }

            public string Value { get; }
        }
    }
}