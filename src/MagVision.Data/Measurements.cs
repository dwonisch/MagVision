using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagVision.Data
{
    public class Measurements
    {
        public Acuity LeftHyperobic { get; set; }
        public Acuity RightHyperobic { get; set; }
        public Acuity LeftMyopic { get; set; }
        public Acuity RightMyopic { get; set; }
        public float Magnification { get; set; }
    }
}
