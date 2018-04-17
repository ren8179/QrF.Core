using System;
using System.Collections.Generic;
using System.Text;

namespace QrF.ABP.Configuration.Startup
{
    internal class EventBusConfiguration : IEventBusConfiguration
    {
        public bool UseDefaultEventBus { get; set; }

        public EventBusConfiguration()
        {
            UseDefaultEventBus = true;
        }
    }
}
