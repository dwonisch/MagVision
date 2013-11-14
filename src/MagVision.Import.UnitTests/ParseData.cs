using System;
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
            data = new string[] { "0", "Mustermann", "Max"  };
            data2 = new string[] { "Dr.", "Quak", "Alfred J." };
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
    }
}
