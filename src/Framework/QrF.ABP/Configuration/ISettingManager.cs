using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QrF.ABP.Configuration
{
    /// <summary>
    /// This is the main interface that must be implemented to be able to load/change values of settings.
    /// </summary>
    public interface ISettingManager
    {
        /// <summary>
        /// Gets current value of a setting.
        /// It gets the setting value, overwritten by application, current tenant and current user if exists.
        /// </summary>
        /// <param name="name">Unique name of the setting</param>
        /// <returns>Current value of the setting</returns>
        Task<string> GetSettingValueAsync(string name);

        /// <summary>
        /// Gets current value of a setting for the application level.
        /// </summary>
        /// <param name="name">Unique name of the setting</param>
        /// <returns>Current value of the setting for the application</returns>
        Task<string> GetSettingValueForApplicationAsync(string name);

        /// <summary>
        /// Gets current value of a setting for the application level.
        /// If fallbackToDefault is false, it just gets value from application and returns null if application has not defined a value for the setting.
        /// </summary>
        /// <param name="name">Unique name of the setting</param>
        /// <param name="fallbackToDefault"></param>
        /// <returns>Current value of the setting for the application</returns>
        Task<string> GetSettingValueForApplicationAsync(string name, bool fallbackToDefault);
        
        /// <summary>
        /// Gets current values of all settings.
        /// It gets all setting values, overwritten by application, current tenant (if exists) and the current user (if exists).
        /// </summary>
        /// <returns>List of setting values</returns>
        Task<IReadOnlyList<ISettingValue>> GetAllSettingValuesAsync();

        /// <summary>
        /// Gets current values of all settings.
        /// It gets default values of all settings then overwrites by given scopes.
        /// </summary>
        /// <param name="scopes">One or more scope to overwrite</param>
        /// <returns>List of setting values</returns>
        Task<IReadOnlyList<ISettingValue>> GetAllSettingValuesAsync(SettingScopes scopes);

        /// <summary>
        /// Gets a list of all setting values specified for the application.
        /// It returns only settings those are explicitly set for the application.
        /// If a setting's default value is used, it's not included the result list.
        /// If you want to get current values of all settings, use <see cref="GetAllSettingValuesAsync()"/> method.
        /// </summary>
        /// <returns>List of setting values</returns>
        Task<IReadOnlyList<ISettingValue>> GetAllSettingValuesForApplicationAsync();
        
        /// <summary>
        /// Changes setting for the application level.
        /// </summary>
        /// <param name="name">Unique name of the setting</param>
        /// <param name="value">Value of the setting</param>
        Task ChangeSettingForApplicationAsync(string name, string value);
        
    }
}
