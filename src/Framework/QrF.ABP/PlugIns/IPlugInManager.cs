using System;
using System.Collections.Generic;
using System.Text;

namespace QrF.ABP.PlugIns
{
    public interface IPlugInManager
    {
        PlugInSourceList PlugInSources { get; }
    }
}
