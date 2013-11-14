using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MagVision.Data;

namespace MagVision.Import.UnitTests
{
    [TestClass]
    public class ParseData
    {
        string[] data;
        string[] data2;
        Importer importer;

        [TestInitialize]
        public void Initialize()
        {
            data = new string[] { "0", "Mustermann", "Max", "Musterstraße 12", "9999", "Musterstadt" };
            data2 = new string[] { "Dr.", "Quak", "Alfred J.", "0", "0", "0"};
            importer = new Importer();
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

        private AddressInformation FirstAddress(Patient patient)
        {
            return patient.Addresses.First();
        }
    }
}
