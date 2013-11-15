using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagVision.Data
{
    public class Measurement
    {
        public DateTime? Date { get; set; }
        public Acuity Hyperobic { get; set; }
        public Acuity Myopic { get; set; }
        public float Magnification { get; set; }

        public override bool Equals(object obj)
        {
            var other = obj as Measurement;
            if (other != null)
            {
                return Equals(Date, other.Date) && Equals(Hyperobic, other.Hyperobic) && Equals(Myopic, other.Myopic) && Equals(Magnification, other.Magnification);
            }
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return 397 ^ Date.GetHashCode() ^ Hyperobic.GetHashCode() ^ Myopic.GetHashCode() ^ Magnification.GetHashCode();
        }
    }
}
