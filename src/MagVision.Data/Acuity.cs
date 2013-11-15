using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagVision.Data
{
    public class Acuity
    {
        public Acuity()
        {
        }

        public Acuity(Eye left, Eye right)
        {
            Left = left;
            Right = right;
        }

        public Eye Left { get; set; }
        public Eye Right { get; set; }

        public override bool Equals(object obj)
        {
            var other = obj as Acuity;
            if (other != null)
            {
                return Equals(Left, other.Left) && Equals(Right, other.Right);
            }

            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return 397 ^ Left.GetHashCode() ^ Right.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format(@"{{
    ""Left"" = ""{0}"",
    ""Right"" = ""{1}""
}}", Left, Right);
        }
    }
}
