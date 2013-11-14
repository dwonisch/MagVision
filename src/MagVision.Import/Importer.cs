using MagVision.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagVision.Import
{
    public class Importer
    {
        public Patient Import(string[] dataFields)
        {
            var patient = new Patient();
            patient.Title = CheckForZero(dataFields[0]);
            patient.Name = dataFields[1];
            patient.FirstName = dataFields[2];

            var address = new AddressInformation();
            address.Street = CheckForZero(dataFields[3]);
            address.PostCode = CheckForZero(dataFields[4]);
            address.City = CheckForZero(dataFields[5]);

            patient.Addresses.Add(address);

            return patient;
        }

        private string CheckForZero(string title)
        {
            if (title == "0")
                return string.Empty;
            else
                return title;
        }
    }
}
