using MagVision.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagVision.Import.Parser
{
    public class MeasurementParser : IParser<Measurement>
    {
        private IParser<Acuity> acuityParser;

        public MeasurementParser(IParser<Acuity> acuityParser)
        {
            this.acuityParser = acuityParser;
        }

        public Measurement Parse(string value)
        {
            throw new NotImplementedException();
        }


        public Measurement Parse(params string[] values)
        {
            var measurement = new Measurement();

            measurement.Hyperobic = acuityParser.Parse(values[0]);

            return measurement;
        }
    }
}
