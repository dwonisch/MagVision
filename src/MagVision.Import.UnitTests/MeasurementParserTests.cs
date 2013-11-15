using MagVision.Data;
using MagVision.Import.Parser;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagVision.Import.UnitTests
{
    [TestClass]
    public class MeasurementParserTests
    {
        [TestMethod]
        public void ParseMeasurement()
        {
            var myopic = new Acuity(new Eye(1,1,1,1), new Eye(2,2,2,2));
            var hyperopic = new Acuity(new Eye(2,2,2,2), new Eye(3,3,3,3));

            var acuityParser = MockRepository.GenerateStub<IParser<Acuity>>();
            acuityParser.Stub(a => a.Parse("A")).Return(myopic);
            acuityParser.Stub(a => a.Parse("B")).Return(hyperopic);

            var factorParser = MockRepository.GenerateStub<IParser<float>>();
            factorParser.Stub(a => a.Parse("3.7x")).Return(3.7f);

            var parser = new MeasurementParser(acuityParser, factorParser);

            Assert.AreEqual(new Measurement(hyperopic, myopic, 3.7f), parser.Parse("B", "A", "3.7x"));
        }
    }
}
