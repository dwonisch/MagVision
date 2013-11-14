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
            patient.Title = ParseTitle(dataFields[0]);
            patient.Name = dataFields[1];
            patient.FirstName = dataFields[2];
            return patient;
        }

        private string ParseTitle(string title)
        {
            if (title == "0")
                return string.Empty;
            else
                return title;
        }
    }
}
