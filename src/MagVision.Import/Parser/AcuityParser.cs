using MagVision.Data;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagVision.Import.Parser
{
    public class AcuityParser : IParser<Acuity>
    {
        private IParser<Eye> eyeParser;

        public AcuityParser(IParser<Eye> eyeParser)
        {
            this.eyeParser = eyeParser;
        }

        public Acuity Parse(string value)
        {
            var acuity = new Acuity();

            value = RemoveFirstPart(value);
            var lines = GetLines(value);

            acuity.Right = eyeParser.Parse(lines.First(l => l.StartsWith("RA")));
            acuity.Left = eyeParser.Parse(lines.First(l => l.StartsWith("LA")));

            return acuity;
        }

        private IEnumerable<string> GetLines(string value)
        {
            return value.Split(new[]{'\n','\r'}, StringSplitOptions.RemoveEmptyEntries).Select(l => l.Trim());
        }

        private string RemoveFirstPart(string value)
        {
            return value.Split(':')[1];
        }


        public Acuity Parse(params string[] values)
        {
            throw new NotImplementedException();
        }
    }
}
