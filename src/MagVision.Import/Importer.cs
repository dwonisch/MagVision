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

            if (!IsZero(dataFields[6]))
            {
                patient.PhoneNumbers.Add(new PhoneNumber(dataFields[6]));
            }

            patient.Birthday = dateParser.Parse(dataFields[7]);
            patient.InsuranceNumber = CheckForZero(dataFields[8]);

            patient.Medic = medicDirectory.Get(dataFields[9]);
            patient.HealthInsurance = healthInsuranceDirectory.Get(dataFields[10]);

            //skip field 11 that should contain lastVisit, but is filled with other data

            patient.Salutation = salutationDirectory.Get(dataFields[12]);
            patient.Identifier = ConvertToInt32(dataFields[13]);

            var insuredPerson = new InsuredPerson();
            insuredPerson.Name = CheckForZero(dataFields[14]);
            insuredPerson.Birthday = dateParser.Parse(dataFields[15]);
            insuredPerson.Address = ParseAddress(CheckForZero(dataFields[16]));
            if(!IsZero(dataFields[17]))
                insuredPerson.PhoneNumbers.Add(new PhoneNumber(dataFields[17]));
            insuredPerson.DegreeOfRelationship = CheckForZero(dataFields[18]);

            patient.InsuredPerson = insuredPerson;

            var findings = new DiagnosticFindings();
            findings.MedicationDate = dateParser.Parse(dataFields[19]);
            findings.Cataract = ConvertToBoolean(dataFields[20]);
            findings.MacularDegeneration = ConvertToBoolean(dataFields[21]);
            findings.RetinopathiaDiabetica = ConvertToBoolean(dataFields[22]);
            findings.Aphakia = ConvertToBoolean(dataFields[23]);
            findings.Edema = ConvertToBoolean(dataFields[24]);
            findings.Hemorrhage = ConvertToBoolean(dataFields[25]);
            findings.AtropiaNerviOptici = ConvertToBoolean(dataFields[26]);

            patient.DiagnosticFindings.Add(findings);
            return patient;
        }

        private bool ConvertToBoolean(string data)
        {
            return data == "1";
        }

        private AddressInformation ParseAddress(string addressString)
        {
            var fields = addressString.Split(' ', '.');
            var address = new AddressInformation();

            if (fields.Length == 4)
            {
                address.Street = string.Format("{0} {1}", fields[0], fields[1]);
                address.PostCode = fields[2];
                address.City = fields[3];
            }
            return address;
        }

        private string CheckForZero(string title)
        {
            if (IsZero(title))
                return string.Empty;
            else
                return title;
        }

        private static bool IsZero(string title)
        {
            return title == "0";
        }

        private int ConvertToInt32(string value)
        {
            return int.Parse(value);
        }
    }
}
