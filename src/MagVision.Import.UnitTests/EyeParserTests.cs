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
    public class EyeParserTests
    {
        [TestMethod]
        public void Test()
        {
            string data = @"RA +1,00 -0,50 150° PD 33,5 ADD +2,5";
            var parser = new EyeParser();
            Assert.AreEqual(new Eye(1f, -0.5f, 150f, 33.5f, 2.5f), parser.Parse(data));
        }
    }
}
