using QrF.ABP.Runtime.Caching;
using System;
using System.Collections.Generic;
using System.Text;

namespace QrF.ABP.Configuration
{
    /// <summary>
    /// Extension methods for <see cref="ICacheManager"/> to get setting caches.
    /// </summary>
    public static class CacheManagerSettingExtensions
    {
        /// <summary>
        /// Application settings cache: AbpApplicationSettingsCache.
        /// </summary>
        public const string ApplicationSettings = "App_SettingsCache";

        /// <summary>
        /// Gets application settings cache.
        /// </summary>
        public static ITypedCache<string, Dictionary<string, SettingInfo>> GetApplicationSettingsCache(this ICacheManager cacheManager)
        {
            return cacheManager
                .GetCache<string, Dictionary<string, SettingInfo>>(ApplicationSettings);
        }

    }
}
