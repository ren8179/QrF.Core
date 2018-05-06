using Castle.Core.Logging;
using Microsoft.AspNetCore.Mvc;
using QrF.ABP.Configuration;
using QrF.ABP.Dependency;
using QrF.ABP.Events.Bus;
using QrF.ABP.ObjectMapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace QrF.ABP.AspNetCore.Mvc.Controllers
{
    /// <summary>
    /// Base class for all MVC Controllers in Abp system.
    /// </summary>
    public abstract class BaseController : Controller, ITransientDependency
    {
        /// <summary>
        /// Gets the event bus.
        /// </summary>
        public IEventBus EventBus { get; set; }
        
        /// <summary>
        /// Reference to the setting manager.
        /// </summary>
        public ISettingManager SettingManager { get; set; }
        
        /// <summary>
        /// Reference to the object to object mapper.
        /// </summary>
        public IObjectMapper ObjectMapper { get; set; }
        
        /// <summary>
        /// Reference to the logger to write logs.
        /// </summary>
        public ILogger Logger { get; set; }
        
        /// <summary>
        /// Constructor.
        /// </summary>
        protected BaseController()
        {
            Logger = NullLogger.Instance;
            EventBus = NullEventBus.Instance;
            ObjectMapper = NullObjectMapper.Instance;
        }
        
    }
}
