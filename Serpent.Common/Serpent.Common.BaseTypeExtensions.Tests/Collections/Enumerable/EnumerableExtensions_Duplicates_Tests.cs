namespace Serpent.Common.BaseTypeExtensions.Tests
{
    using System;
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Serpent.Common.BaseTypeExtensions.Collections;

    /// <summary>
    ///     Summary description for EnumerableExtensionsTests
    /// </summary>
    [TestClass]
    public class EnumerableExtensions_Duplicates_Tests
    {
        public TestContext TestContext { get; set; }


        private class TestClass
        {
            public TestClass(string name)
            {
                this.Name = name;
            }

            public TestClass(string name, int value)
            {
                this.Name = name;
                this.Value = value;
            }


            public string Name { get; }

            public int Value { get; }

            public Guid Id { get; } = Guid.NewGuid();

        }


        [TestMethod]
        public void Duplicates_From_Class()
        {
            var sven = new TestClass("Sven");
            var ove = new TestClass("Ove");
            var knut = new TestClass("Knut");
            var bo = new TestClass("Bo");

            var items = new[] { bo, sven, ove, knut, ove, ove, bo, };

            var duplicates = items.Duplicates().ToArray();

            Assert.IsTrue(duplicates.Any(d => d == bo));
            Assert.IsTrue(duplicates.Any(d => d == ove));

            Assert.IsFalse(duplicates.Any(d => d == knut));
            Assert.IsFalse(duplicates.Any(d => d == sven));
        }

        [TestMethod]
        public void Duplicates_From_String()
        {
            var duplicates = new[] { "Bo", "Ove", "Bo", "Knut", "Sven" }.Duplicates().ToArray();

            Assert.IsTrue(duplicates.Any(d => d == "Bo"));
            Assert.IsFalse(duplicates.Any(d => d == "Ove" || d == "Knut" || d == "Sven"));
        }

        [TestMethod]
        public void Duplicates_From_Class_Selector()
        {
            var sven = new TestClass("Sven", 1);
            var ove = new TestClass("Ove", 2);
            var knut = new TestClass("Knut", 2);
            var bo = new TestClass("Bo", 3);

            var items = new[] { sven, ove, knut, bo };

            var duplicates = items.Duplicates(i => i.Value).ToArray();

            Assert.IsTrue(duplicates.Any(d => d.Value == 2));

            Assert.IsFalse(duplicates.Any(d => d.Value == 1));
            Assert.IsFalse(duplicates.Any(d => d.Value == 3));
        }

        // You can use the following additional attributes as you write your tests:
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
    }
}