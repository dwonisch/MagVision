using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagVision.Data
{
    public class Eye
    {
        public Eye()
        {
        }

        public Eye(float sphere, float cylinder, float axis, float eyeDistance) : this(sphere, cylinder, axis, eyeDistance, 0)
        {
        }

        public Eye(float sphere, float cylinder, float axis, float eyeDistance, float addition)
        {
            Sphere = sphere;
            Cylinder = cylinder;
            Axis = axis;
            EyeDistance = eyeDistance;
            Addition = addition;
        }

        public float Sphere { get; set; }
        public float Cylinder { get; set; }
        public float Axis { get; set; }
        public float EyeDistance { get; set; }
        public float Addition { get; set; }

        public override bool Equals(object obj)
        {
            var other = obj as Eye;
            if (other != null)
            {
                return Equals(Sphere, other.Sphere) && Equals(Cylinder, other.Cylinder) && Equals(Axis, other.Axis) && Equals(EyeDistance, other.EyeDistance) && Equals(Addition, other.Addition);
            }

            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return 397 ^ Sphere.GetHashCode() ^ Cylinder.GetHashCode() ^ Axis.GetHashCode() ^ EyeDistance.GetHashCode() ^ EyeDistance.GetHashCode() ^ Addition.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format(@"{{ ""Sphere"" = ""{0}"", ""Cylinder"" = ""{1}"", ""Axis"" = ""{2}"", ""EyeDistance"" = ""{3}"", ""Addition"" = ""{4}"" }}", Sphere, Cylinder, Axis, EyeDistance, Addition);
        }
    }
}
