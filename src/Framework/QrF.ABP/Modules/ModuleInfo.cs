using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace QrF.ABP.Modules
{
    /// <summary>
    /// Used to store all needed information for a module.
    /// </summary>
    public class ModuleInfo
    {
        /// <summary>
        /// The assembly which contains the module definition.
        /// </summary>
        public Assembly Assembly { get; }

        /// <summary>
        /// Type of the module.
        /// </summary>
        public Type Type { get; }

        /// <summary>
        /// Instance of the module.
        /// </summary>
        public BaseModule Instance { get; }

        /// <summary>
        /// Is this module loaded as a plugin.
        /// </summary>
        public bool IsLoadedAsPlugIn { get; }

        /// <summary>
        /// All dependent modules of this module.
        /// </summary>
        public List<ModuleInfo> Dependencies { get; }

        /// <summary>
        /// Creates a new AbpModuleInfo object.
        /// </summary>
        public ModuleInfo(Type type, BaseModule instance, bool isLoadedAsPlugIn)
        {
            Type = type;
            Instance = instance;
            IsLoadedAsPlugIn = isLoadedAsPlugIn;
            Assembly = Type.GetTypeInfo().Assembly;

            Dependencies = new List<ModuleInfo>();
        }

        public override string ToString()
        {
            return Type.AssemblyQualifiedName ??
                   Type.FullName;
        }
    }
}
