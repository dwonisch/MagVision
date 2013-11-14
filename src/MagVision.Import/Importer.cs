﻿using MagVision.Data;
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

        public Importer(IParser<DateTime?> dateParser, Directory<Medic> medicDirectory)
        {
            this.dateParser = dateParser;
            this.medicDirectory = medicDirectory;
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
