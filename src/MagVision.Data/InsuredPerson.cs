﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagVision.Data
{
    public class InsuredPerson
    {
        public string FirstName { get; set; }
        public string Name { get; set; }
        public DateTime? Birthday { get; set; }
        public AddressInformation Address { get; set; }
        public List<PhoneNumber> PhoneNumber { get; set; }
        public string DegreeOfRelationship { get; set; }
    }
}
