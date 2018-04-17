using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace QrF.ABP.PlugIns
{
    public interface IPlugInSource
    {
        List<Assembly> GetAssemblies();

        List<Type> GetModules();
    }
}
