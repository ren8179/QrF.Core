using Castle.Core.Logging;
using QrF.ABP.Configuration;
using QrF.ABP.ObjectMapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace QrF.ABP
{
    /// <summary>
    /// This class can be used as a base class for services.
    /// It has some useful objects property-injected and has some basic methods
    /// most of services may need to.
    /// </summary>
    public abstract class ServiceBase
    {
        /// <summary>
        /// Reference to the setting manager.
        /// </summary>
        public ISettingManager SettingManager { get; set; }
        
        /// <summary>
        /// Reference to the logger to write logs.
        /// </summary>
        public ILogger Logger { protected get; set; }

        /// <summary>
        /// Reference to the object to object mapper.
        /// </summary>
        public IObjectMapper ObjectMapper { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        protected ServiceBase()
        {
            Logger = NullLogger.Instance;
            ObjectMapper = NullObjectMapper.Instance;
        }
        
    }
}
