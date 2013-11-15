using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagVision.Data
{
    public class Patient : Person
    {
        public Patient()
        {
            Addresses = new List<AddressInformation>();
        }

        public int Identifier { get; set; }
        public Salutation Salutation { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string InsuranceNumber { get; set; }
        public DateTime? Birthday { get; set; }
        public List<AddressInformation> Addresses { get; set; }
        public Medic Medic { get; set; }
        public HealthInsurance HealthInsurance { get; set; }
        public DateTime? LastVisit { get; set; }
        public InsuredPerson InsuredPerson { get; set; }
    }
}
