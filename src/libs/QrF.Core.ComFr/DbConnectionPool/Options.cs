using System;
using System.Collections.Generic;
using System.Text;

namespace QrF.Core.ComFr.DbConnectionPool
{
    public class Options
    {
        public Options() { MaximumRetained = Environment.ProcessorCount * 2; }
        public int MaximumRetained { get; set; }
    }
}
