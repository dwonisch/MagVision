using MagVision.Data;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagVision.Import.Parser
{
    public class MeasurementParser : IParser<Measurement>
    {
        private IParser<Acuity> acuityParser;
        private IParser<float> factorParser;

        public MeasurementParser(IParser<Acuity> acuityParser, IParser<float> factorParser)
        {
            this.acuityParser = acuityParser;
            this.factorParser = factorParser;
        }

        public Measurement Parse(string value)
        {
            throw new NotImplementedException();
        }


        public Measurement Parse(params string[] values)
        {
            var measurement = new Measurement();

            measurement.Hyperobic = acuityParser.Parse(values[0]);
            measurement.Myopic = acuityParser.Parse(values[1]);
            measurement.Magnification = factorParser.Parse(values[2]);

            return measurement;
        }
    }
}
