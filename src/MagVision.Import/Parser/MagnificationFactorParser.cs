using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MagVision.Import.Parser
{
    public class MagnificationFactorParser : IParser<float>
    {
        private Regex regex = new Regex(@"([0-9][0-9]*[,]*[0-9]*)");

        public float Parse(string value)
        {
            return float.Parse(GetNumber(value), CultureInfo.CurrentUICulture);
        }

        private string GetNumber(string value)
        {
            var matches = regex.Matches(value);

            if (matches.Count == 0)
                return "1";
            else if (matches.Count == 1)
                return matches[0].Value;
            else
                throw new ArgumentException("Input string could not be parsed. Would result in multiple possible values");
        }

        public float Parse(params string[] values)
        {
            throw new NotImplementedException();
        }
    }
}
