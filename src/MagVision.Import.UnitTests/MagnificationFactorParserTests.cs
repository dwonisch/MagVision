using MagVision.Import.Parser;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagVision.Import.UnitTests
{
    [TestClass]
    public class MagnificationFactorParserTests
    {
        private MagnificationFactorParser parser;

        [TestInitialize]
        public void Initalize()
        {
            parser = new MagnificationFactorParser();
        }

        [TestMethod]
        public void TestFactor2()
        {
            Assert.AreEqual(2.0, parser.Parse("2x"));
        }

        [TestMethod]
        public void TestFactor3Comma7()
        {
            Assert.AreEqual(3.7f, parser.Parse("3,7x"), 0.01d);
        }

        [TestMethod]
        public void TestFactor5SpaceX()
        {
            Assert.AreEqual(5f, parser.Parse("5 X"), 0.01d);
        }

        [TestMethod]
        public void TestFactorTextWithNumber()
        {
            Assert.AreEqual(2.5f, parser.Parse("lt. Arzt   2,5 x"), 0.01d);
        }

        [TestMethod]
        public void TestFactorTotalWithNumber()
        {
            Assert.AreEqual(7f, parser.Parse("gesamt 7x"), 0.01d);
        }

        [TestMethod]
        public void TestFactorNumberWithText()
        {
            Assert.AreEqual(2.5f, parser.Parse(@"2,5 x  mit Nahprille"), 0.01d);
        }

        [TestMethod]
        public void TestFactorWithTwoNumbers()
        {
            ExceptionAssert.Throws<ArgumentException>(() => parser.Parse(@"3 X zum Arbeiten
7 X zum Lesen"));
        }


        [TestMethod]
        public void TestFactorEmpty()
        {
            Assert.AreEqual(1f, parser.Parse(string.Empty), 0.01d);
        }
    }
}
