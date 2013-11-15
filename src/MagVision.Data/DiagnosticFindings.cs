using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagVision.Data
{
    public class DiagnosticFindings
    {
        public DateTime? MedicationDate { get; set; }
        public bool Cataract { get; set; }
        public bool MacularDegeneration { get; set; }
        public bool RetinopathiaDiabetica { get; set; }
        public bool Aphakia { get; set; }
        public bool Edema { get; set; }
        public bool Hemorrhage { get; set; }
        public bool AtropiaNerviOptici { get; set; }
        public string Else { get; set; }
    }
}
