using MagVision.Data;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagVision.Import.Parser
{
    public class EyeParser : IParser<Eye>
    {
        public Eye Parse(string value)
        {
            var eye = new Eye();
            var parts = value.Split(new[]{' '}, StringSplitOptions.RemoveEmptyEntries );

            eye.Sphere = float.Parse(parts[1], CultureInfo.CurrentUICulture);
            eye.Cylinder = float.Parse(parts[2], CultureInfo.CurrentUICulture);
            eye.Axis = float.Parse(parts[3].Replace("°", ""), CultureInfo.CurrentUICulture);
            eye.EyeDistance = float.Parse(parts[5], CultureInfo.CurrentUICulture);
            if (parts.Length > 7)
                eye.Addition = float.Parse(parts[7], CultureInfo.CurrentUICulture);

            return eye;
        }

        public Eye Parse(params string[] values)
        {
            throw new NotImplementedException();
        }
    }
}
