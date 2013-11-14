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
            data = new string[] { "0", "Mustermann", "Max", "Musterstraße 12", "9999", "Musterstadt", "0666 999 999 999", "17.05.1938", "1234", "1", "2", "0", "2" };
            data2 = new string[] { "Dr.", "Quak", "Alfred J.", "0", "0", "0", "0", "13 08 1938", "0", "2", "1", "0", "3"};
            var fakeDateParser = MockRepository.GenerateStub<IParser<DateTime?>>();
            fakeDateParser.Stub(d => d.Parse(Arg<string>.Is.Anything)).Return(new DateTime(2000, 12, 15));

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
            Assert.AreEqual(string.Empty, FirstNumber(importer.Import(data2)).Number);
        }

        [TestMethod]
        public void InterpretDateTimeWithSubmittedParser()
        {
            Assert.AreEqual(new DateTime(2000,12,15), importer.Import(data).Birthday);
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

        private AddressInformation FirstAddress(Patient patient)
        {
            return patient.Addresses.First();
        }

        private PhoneNumber FirstNumber(Patient patient)
        {
            return patient.PhoneNumbers.First();
        }
    }
}
