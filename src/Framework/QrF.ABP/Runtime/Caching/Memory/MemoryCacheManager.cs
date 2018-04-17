using Castle.Core.Logging;
using QrF.ABP.Dependency;
using QrF.ABP.Runtime.Caching.Configuration;

namespace QrF.ABP.Runtime.Caching.Memory
{
    /// <summary>
    /// Implements <see cref="ICacheManager"/> to work with MemoryCache.
    /// </summary>
    public class MemoryCacheManager : CacheManagerBase
    {
        public ILogger Logger { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        public MemoryCacheManager(IIocManager iocManager, ICachingConfiguration configuration)
            : base(iocManager, configuration)
        {

        }

        protected override ICache CreateCacheImplementation(string name)
        {
            return new MemoryCache(name)
            {
                Logger = Logger
            };
        }

        protected override void DisposeCaches()
        {
            foreach (var cache in Caches.Values)
            {
                cache.Dispose();
            }
        }
    }
}
