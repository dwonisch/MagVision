using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagVision.Data
{
    public class Person
    {
        public Person()
        {
            PhoneNumbers = new List<PhoneNumber>();
        }

        public List<PhoneNumber> PhoneNumbers { get; set; }
    }
}
