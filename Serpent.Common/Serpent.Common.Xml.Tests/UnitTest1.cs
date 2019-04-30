namespace Serpent.Common.Xml.Tests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class UnitTest1
    {

        [TestMethod]
        public async Task TestFileParsing()
        {
            var stream = this.GetProductStream();

            var reader = new XmlFileParser<ParseContext>(
                ElementDictionary,
                EndElementDictionary,
                AnyTagHandler);

            var context = new ParseContext();

            await reader.ReadXmlFileAsync(stream, context);

            Assert.AreEqual("LINJELASER 2 LINJER DW088K-XJ MED NIVELLERINGSCYKLOP", context.Title);
            Assert.AreEqual(2, context.ItemTitleCount);
            Assert.AreEqual(1, context.EndOfProductCount);
            Assert.AreEqual(1, context.ItemPtiCount);
            Assert.AreEqual(1, context.EndOfItemBranchesCount);
        }

        private static Dictionary<string, Action<ParseContext>> EndElementDictionary
        {
            get
            {
                return new Dictionary<string, Action<ParseContext>>
                {
                    ["/Product"] = ctx => { ctx.EndOfProductCount++; },
                    ["?^/Product/Items/Item/ItemBranch$"] = ctx => { ctx.EndOfItemBranchesCount++; }
                };
            }
        }

        private static Dictionary<string, Action<ParseContext, string>> ElementDictionary
        {
            get
            {
                return new Dictionary<string, Action<ParseContext, string>>
                {
                    // Wildcard match
                    ["?/Product/Items/Item/ItemTitle\\d*$"] = (ctx, title) =>
                        {
                            if (ctx.ItemTitleCount < 2)
                            {
                                ctx.Title = ctx.Title ?? string.Empty;
                                ctx.Title = ctx.Title + (ctx.Title.Length > 0 ? " " : string.Empty) + title;
                            }

                            ctx.ItemTitleCount++;
                        },
                    ["/Product/Items/Item/ItemPTI"] = (ctx, s1) => { ctx.ItemPtiCount++; }
                };
            }
        }

        [TestMethod]
        public async Task TestMultiParsing()
        {
            var reader = new XmlFileParser<ParseContext>(
                ElementDictionary,
                EndElementDictionary,
                AnyTagHandler);

            var context = new ParseContext();

            var multiplier = 10000;

            for (var i = 0; i < multiplier; i++)
            {
                var stream = this.GetProductStream();
                await reader.ReadXmlFileAsync(stream, context);
            }

            Assert.AreEqual(multiplier * 2, context.ItemTitleCount);
            Assert.AreEqual(multiplier * 1, context.EndOfProductCount);
            Assert.AreEqual(multiplier * 1, context.ItemPtiCount);
            Assert.AreEqual(multiplier * 1, context.EndOfItemBranchesCount);
        }

        private static bool AnyTagHandler(string path, XmlTextReader reader)
        {
            if (path.StartsWith("/Product/Items/Item/ItemTextHead"))
            {
                var x = reader.ReadElementContentAsString();
            }

            return true;
        }

        private MemoryStream GetProductStream()
        {
            return new MemoryStream(Encoding.UTF8.GetBytes(XmlSampleData.Product505603));
        }

        internal class ParseContext
        {
            public int EndOfProductCount { get; set; }

            public int ItemPtiCount { get; set; }

            public int EndOfItemBranchesCount { get; set; }

            public int ItemTitleCount { get; set; }

            public string Title { get; set; }
        }
    }
}