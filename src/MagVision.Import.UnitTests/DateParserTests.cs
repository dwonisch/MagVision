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
    public class DateParserTests
    {
        DateParser parser;

        [TestInitialize]
        public void Initialize()
        {
            parser = new DateParser();
        }

        [TestMethod]
        public void ParseDateTimeSeparatedByDots()
        {
            Assert.AreEqual(new DateTime(2013, 11, 14), parser.Parse("14.11.2013"));
        }

        [TestMethod]
        public void ParseDateTimeSeparatedBySpaces()
        {
            Assert.AreEqual(new DateTime(2013, 11, 14), parser.Parse("14 11 2013"));
        }

        [TestMethod]
        public void ParseShortDateTimeSeparatedBySpaces()
        {
            Assert.AreEqual(new DateTime(1915,05,26), parser.Parse("26 05 15"));
        }

        [TestMethod]
        public void ParseShortDateTimeSeparatedBySpacesFromOver100Years()
        {
            Assert.AreEqual(new DateTime(1911, 10, 30), parser.Parse("30 10 11"));
        }

        [TestMethod]
        public void ParseShortDateTimeSeparatedByDots()
        {
            Assert.AreEqual(new DateTime(1915, 05, 26), parser.Parse("26.05.15"));
        }

        [TestMethod]
        public void ParseEmptyStringAsNull()
        {
            Assert.AreEqual(null, parser.Parse(string.Empty));
        }
    }
}
