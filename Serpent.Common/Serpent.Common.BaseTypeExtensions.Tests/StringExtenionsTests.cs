using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serpent.Common.BaseTypeExtensions.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class StringExtensionsTests
    {
        [TestMethod]
        public void ToInt_Test()
        {
            int value = "abc".ToInt();
            Assert.AreEqual(default(int), value);

        }
    }
}
