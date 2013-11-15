using MagVision.Data;
using MagVision.Import.Directories;
using MagVision.Import.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagVision.Import
{
    public class Importer
    {
        private IParser<DateTime?> dateParser;
        private Directory<Medic> medicDirectory;
        private Directory<HealthInsurance> healthInsuranceDirectory;
        private Directory<Salutation> salutationDirectory;

        public Importer(IParser<DateTime?> dateParser, Directory<Medic> medicDirectory, Directory<HealthInsurance> healthInsuranceDirectory, Directory<Salutation> salutationDirectory)
        {
            this.dateParser = dateParser;
            this.medicDirectory = medicDirectory;
            this.healthInsuranceDirectory = healthInsuranceDirectory;
            this.salutationDirectory = salutationDirectory;
        }

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

            var phoneNumber = new PhoneNumber();
            phoneNumber.Number = CheckForZero(dataFields[6]);
            patient.PhoneNumbers.Add(phoneNumber);

            patient.Birthday = dateParser.Parse(dataFields[7]);
            patient.InsuranceNumber = CheckForZero(dataFields[8]);

            patient.Medic = medicDirectory.Get(dataFields[9]);
            patient.HealthInsurance = healthInsuranceDirectory.Get(dataFields[10]);

            //skip field 11 that should contain lastVisit, but is filled with other data

            patient.Salutation = salutationDirectory.Get(dataFields[12]);
            patient.Identifier = ConvertToInt32(dataFields[13]);

            var insuredPerson = new InsuredPerson();
            insuredPerson.Name = CheckForZero(dataFields[14]);

            patient.InsuredPerson = insuredPerson;

            return patient;
        }

        private string CheckForZero(string title)
        {
            if (title == "0")
                return string.Empty;
            else
                return title;
        }

        private int ConvertToInt32(string value)
        {
            return int.Parse(value);
        }
    }
}
