using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MagVision.Data;
using MagVision.Import.Parser;
using Rhino.Mocks;
using MagVision.Import.Directories;

namespace MagVision.Import.UnitTests
{
    [TestClass]
    public class ParsePatient
    {
        string[] data;
        string[] data2;
        Importer importer;

        [TestInitialize]
        public void Initialize()
        {
            data = new string[] { "0", "Mustermann", "Max", "Musterstraße 12", "9999", "Musterstadt", "0666 999 999 999", "17.05.1938", "1234", "1", "2", "0", "2", "1234", "Frau Mustermann", "31 05 90", "Entenhausen.12 4444 Europe", "0", "0", "0" };
            data2 = new string[] { "Dr.", "Quak", "Alfred J.", "0", "0", "0", "0", "13 08 1938", "0", "2", "1", "0", "3", "12345", "0", "0", "0", "0666 999 888 777", "Mutter","01 01 2013" };
            var fakeDateParser = MockRepository.GenerateStub<IParser<DateTime?>>();
            fakeDateParser.Stub(d => d.Parse("17.05.1938")).Return(new DateTime(1938,5,17));
            fakeDateParser.Stub(d => d.Parse("13 08 1938")).Return(new DateTime(1938,8,13));
            fakeDateParser.Stub(d => d.Parse("31 05 90")).Return(new DateTime(1990, 5, 31));
            fakeDateParser.Stub(d => d.Parse("01 01 2013")).Return(new DateTime(2013,1,1));

            var medicDirectory = new Directory<Medic>();
            medicDirectory.Add(1, new Medic("Dr. Kurz"));
            medicDirectory.Add(2, new Medic("Dr. Lang"));

            var healthInsuranceDirectory = new Directory<HealthInsurance>();
            healthInsuranceDirectory.Add(1, new HealthInsurance("A"));
            healthInsuranceDirectory.Add(2, new HealthInsurance("B"));

            var salutationDirectory = new Directory<Salutation>();
            salutationDirectory.Add(1, new Salutation("Herr"));
            salutationDirectory.Add(2, new Salutation("Frau"));
            salutationDirectory.Add(3, new Salutation("Firma"));

            importer = new Importer(fakeDateParser, medicDirectory, healthInsuranceDirectory, salutationDirectory);
        }

        [TestMethod]
        public void InterpretTitleAsRead()
        {
            Assert.AreEqual("Dr.", importer.Import(data2).Title);
        }

        [TestMethod]
        public void InterpretTitle0AsNone()
        {
            Assert.AreEqual(string.Empty, importer.Import(data).Title);
        }

        [TestMethod]
        public void InterpretNameAsRead()
        {
            Assert.AreEqual("Mustermann", importer.Import(data).Name);
        }

        [TestMethod]
        public void InterpretFirstNameAsRead()
        {
            Assert.AreEqual("Max", importer.Import(data).FirstName);
        }

        [TestMethod]
        public void InterpretStreetAsRead()
        {
            Assert.AreEqual("Musterstraße 12", FirstAddress(importer.Import(data)).Street);
        }

        [TestMethod]
        public void InterpretStreet0AsEmpty()
        {
            Assert.AreEqual(string.Empty, FirstAddress(importer.Import(data2)).Street);
        }

        [TestMethod]
        public void InterpretPostCodeAsRead()
        {
            Assert.AreEqual("9999", FirstAddress(importer.Import(data)).PostCode);
        }

        [TestMethod]
        public void InterpretPostCode0AsEmpty()
        {
            Assert.AreEqual(string.Empty, FirstAddress(importer.Import(data2)).PostCode);
        }

        [TestMethod]
        public void InterpretCityAsRead()
        {
            Assert.AreEqual("Musterstadt", FirstAddress(importer.Import(data)).City);
        }

        [TestMethod]
        public void InterpretCity0AsEmpty()
        {
            Assert.AreEqual(string.Empty, FirstAddress(importer.Import(data2)).City);
        }

        [TestMethod]
        public void InterpretPhoneNumberAsRead()
        {
            Assert.AreEqual("0666 999 999 999", FirstNumber(importer.Import(data)).Number);
        }

        [TestMethod]
        public void InterpretPhoneNumber0AsEmpty()
        {
            Assert.AreEqual(0, importer.Import(data2).PhoneNumbers.Count);
        }

        [TestMethod]
        public void InterpretDateTimeWithSubmittedParser()
        {
            Assert.AreEqual(new DateTime(1938,5,17), importer.Import(data).Birthday);
        }

        [TestMethod]
        public void InterpretInsuranceNumberAsRead()
        {
            Assert.AreEqual("1234", importer.Import(data).InsuranceNumber);
        }

        [TestMethod]
        public void InterpretInsuranceNumber0AsEmpty()
        {
            Assert.AreEqual(string.Empty, importer.Import(data2).InsuranceNumber);
        }

        [TestMethod]
        public void InterpretMedicCorrectly()
        {
            Assert.AreEqual("Dr. Kurz", importer.Import(data).Medic.Name);
        }

        [TestMethod]
        public void InterpretMedicCorrectlyData2()
        {
            Assert.AreEqual("Dr. Lang", importer.Import(data2).Medic.Name);
        }

        [TestMethod]
        public void InterpretHealthInsuranceCorrectly()
        {
            Assert.AreEqual("B", importer.Import(data).HealthInsurance.Name);
        }

        [TestMethod]
        public void InterpretHealthInsuranceCorrectlyData2()
        {
            Assert.AreEqual("A", importer.Import(data2).HealthInsurance.Name);
        }

        /// <summary>
        /// Do not read this value because it is used in another way. Leave it as default.
        /// </summary>
        [TestMethod]
        public void DoNotInterpretLastCheck()
        {
            Assert.AreEqual(null, importer.Import(data).LastVisit);
        }

        [TestMethod]
        public void InterpretSalutationCorrectly()
        {
            Assert.AreEqual("Firma", importer.Import(data2).Salutation.Name);
        }

        [TestMethod]
        public void InterpretIdentifierAsRead()
        {
            Assert.AreEqual(1234, importer.Import(data).Identifier);
        }

        [TestMethod]
        public void InterpretInsuredPersonNameAsRead()
        {
            Assert.AreEqual("Frau Mustermann", importer.Import(data).InsuredPerson.Name);
        }

        [TestMethod]
        public void InterpretInsuredPerson0AsEmpty()
        {
            Assert.AreEqual(string.Empty, importer.Import(data2).InsuredPerson.Name);
        }

        [TestMethod]
        public void InterpretInsuredPersonBirthdayAsRead()
        {
            Assert.AreEqual(new DateTime(1990,5,31), importer.Import(data).InsuredPerson.Birthday);
        }

        [TestMethod]
        public void InterpretInsuredPersonAddressAsZero()
        {
            var expected = new AddressInformation(){
                Street = "Entenhausen 12",
                PostCode = "4444",
                City = "Europe"
            };
            Assert.AreEqual(expected, importer.Import(data).InsuredPerson.Address);
        }

        [TestMethod]
        public void InterpretInsuredPersonAddress0AsEmpty()
        {
            Assert.AreEqual(new AddressInformation(), importer.Import(data2).InsuredPerson.Address);
        }

        [TestMethod]
        public void InterpretInsuredPersonPhoneNumber0AsEmpty()
        {
            Assert.AreEqual(0, importer.Import(data).InsuredPerson.PhoneNumbers.Count);
        }

        [TestMethod]
        public void InterpretInsuredPersonPhoneNumberAsRead()
        {
            Assert.AreEqual("0666 999 888 777", FirstNumber(importer.Import(data2).InsuredPerson).Number);
        }

        [TestMethod]
        public void InterpretInsuredPersonRelationshipAsRead()
        {
            Assert.AreEqual("Mutter", importer.Import(data2).InsuredPerson.DegreeOfRelationship);
        }

        [TestMethod]
        public void InterpretInsuredPersonRelationShip0AsEmpty()
        {
            Assert.AreEqual(string.Empty, importer.Import(data).InsuredPerson.DegreeOfRelationship);
        }

        [TestMethod]
        public void InterpretMedicationDateAsRead()
        {
            Assert.AreEqual(new DateTime(2013, 01, 01), importer.Import(data2).DiagnosticFindings.First().MedicationDate);
        }

        private AddressInformation FirstAddress(Patient patient)
        {
            return patient.Addresses.First();
        }

        private PhoneNumber FirstNumber(Person patient)
        {
            return patient.PhoneNumbers.First();
        }
    }
}
