using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagVision.Data
{
    public class AddressInformation
    {
        public string PostCode { get; set; }
        public string Street { get; set; }
        public string City { get; set; }

        public override bool Equals(object obj)
        {
            var other = obj as AddressInformation;
            if (other != null)
                return PostCode == other.PostCode && Street == other.Street && City == other.City;
            
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return 397 ^ PostCode.GetHashCode() ^ Street.GetHashCode() ^ City.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format(@"{{ ""PostCode"" = ""{0}"", ""Street"" = ""{1}"", ""City"" = ""{2}"" }}", PostCode, Street, City);
        }
    }
}
