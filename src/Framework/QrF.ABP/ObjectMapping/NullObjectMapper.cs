using QrF.ABP.Dependency;
using System;
using System.Collections.Generic;
using System.Text;

namespace QrF.ABP.ObjectMapping
{
    public sealed class NullObjectMapper : IObjectMapper, ISingletonDependency
    {
        /// <summary>
        /// Singleton instance.
        /// </summary>
        public static NullObjectMapper Instance { get { return SingletonInstance; } }
        private static readonly NullObjectMapper SingletonInstance = new NullObjectMapper();

        public TDestination Map<TDestination>(object source)
        {
            throw new BaseException("Abp.ObjectMapping.IObjectMapper should be implemented in order to map objects.");
        }

        public TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
        {
            throw new BaseException("Abp.ObjectMapping.IObjectMapper should be implemented in order to map objects.");
        }
    }
}
