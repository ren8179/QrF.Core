using QrF.ABP.Logging;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Threading.Tasks;

namespace QrF.ABP.Configuration
{
    /// <summary>
    /// Implements default behavior for ISettingStore.
    /// Only <see cref="GetSettingOrNullAsync"/> method is implemented and it gets setting's value
    /// from application's configuration file if exists, or returns null if not.
    /// </summary>
    public class DefaultConfigSettingStore : ISettingStore
    {
        /// <summary>
        /// Gets singleton instance.
        /// </summary>
        public static DefaultConfigSettingStore Instance { get; } = new DefaultConfigSettingStore();
        private DefaultConfigSettingStore()
        {
        }

        public Task<SettingInfo> GetSettingOrNullAsync(int? tenantId, long? userId, string name)
        {
            var value = ConfigurationManager.AppSettings[name];

            if (value == null)
            {
                return Task.FromResult<SettingInfo>(null);
            }

            return Task.FromResult(new SettingInfo(tenantId, userId, name, value));

        }
        /// <inheritdoc/>
        public Task DeleteAsync(SettingInfo setting)
        {
            LogHelper.Logger.Warn("ISettingStore is not implemented, using DefaultConfigSettingStore which does not support DeleteAsync.");
            return Task.FromResult(0);
        }

        /// <inheritdoc/>
        public Task CreateAsync(SettingInfo setting)
        {
            LogHelper.Logger.Warn("ISettingStore is not implemented, using DefaultConfigSettingStore which does not support CreateAsync.");
            return Task.FromResult(0);
        }

        /// <inheritdoc/>
        public Task UpdateAsync(SettingInfo setting)
        {
            LogHelper.Logger.Warn("ISettingStore is not implemented, using DefaultConfigSettingStore which does not support UpdateAsync.");
            return Task.FromResult(0);
        }

        /// <inheritdoc/>
        public Task<List<SettingInfo>> GetAllListAsync(int? tenantId, long? userId)
        {
            LogHelper.Logger.Warn("ISettingStore is not implemented, using DefaultConfigSettingStore which does not support GetAllListAsync.");
            return Task.FromResult(new List<SettingInfo>());
        }
    }
}
