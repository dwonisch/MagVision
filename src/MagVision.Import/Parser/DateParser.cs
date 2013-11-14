using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagVision.Import.Parser
{
    public class DateParser : IParser<DateTime?>
    {
        private static DateTime twoDigitYearUpperBorder = new DateTime(2000, 01, 01);

        public DateTime? Parse(string value)
        {
            if (string.IsNullOrEmpty(value))
                return null;
            var date = DateTime.Parse(value);
            if (date >= twoDigitYearUpperBorder && !DateStringContainsFourDigitYear(value, date))
                date = date.AddYears(-100);
            return date;
        }

        private static bool DateStringContainsFourDigitYear(string value, DateTime date)
        {
            return value.Contains(date.Year.ToString());
        }
    }
}
