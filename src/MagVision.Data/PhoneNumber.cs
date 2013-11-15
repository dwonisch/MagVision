using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagVision.Data
{
    public class PhoneNumber
    {
        public PhoneNumber(string number)
        {
            Number = number;
        }

        public string Number { get; set; }
    }
}
