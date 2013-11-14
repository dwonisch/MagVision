using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagVision.Import.Directories
{
    public class Directory<T> : Dictionary<int,T>
    {
        public virtual T Get(string identifier)
        {
            int id = int.Parse(identifier);
            return this[id];
        }
    }
}
