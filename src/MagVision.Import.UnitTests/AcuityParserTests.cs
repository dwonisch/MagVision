using MagVision.Data;
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
    public class AcuityParserTests
    {
        [TestMethod]
        public void SimpleTestData()
        {
            string data = @"Ferne:RA +1,00 -0,50 150° PD 33,5
          LA +1,50 -0,25 165° PD 31,5  ADD +2,50";
            var parser = new AcuityParser(new EyeParser());

            Assert.AreEqual(new Acuity(new Eye(1.5f, -0.25f, 165f, 31.5f, 2.5f), new Eye(1f, -0.5f, 150f, 33.5f)), parser.Parse(data));
        }
    }
}
