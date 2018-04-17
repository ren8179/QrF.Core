using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace QrF.ABP.PlugIns
{
    public static class PlugInSourceListExtensions
    {
        public static void AddFolder(this PlugInSourceList list, string folder, SearchOption searchOption = SearchOption.TopDirectoryOnly)
        {
            list.Add(new FolderPlugInSource(folder, searchOption));
        }

        public static void AddTypeList(this PlugInSourceList list, params Type[] moduleTypes)
        {
            list.Add(new PlugInTypeListSource(moduleTypes));
        }
    }
}
