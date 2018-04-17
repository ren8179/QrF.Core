using System;
using System.Collections.Generic;
using System.Text;

namespace QrF.ABP.Modules
{
    /// <summary>
    /// Used to define dependencies of an Base module to other modules.
    /// It should be used for a class derived from <see cref="BaseModule"/>.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class DependsOnAttribute : Attribute
    {
        /// <summary>
        /// Types of depended modules.
        /// </summary>
        public Type[] DependedModuleTypes { get; private set; }

        /// <summary>
        /// Used to define dependencies of an Base module to other modules.
        /// </summary>
        /// <param name="dependedModuleTypes">Types of depended modules</param>
        public DependsOnAttribute(params Type[] dependedModuleTypes)
        {
            DependedModuleTypes = dependedModuleTypes;
        }
    }
}
