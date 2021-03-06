﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagVision.Import.Parser
{
    public interface IParser<T>
    {
        T Parse(string value);
        T Parse(params string[] values);
    }
}
