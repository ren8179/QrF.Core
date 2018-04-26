using Castle.Facilities.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace QrF.ABP.Castle.Log4Net
{
    public static class LoggingFacilityExtensions
    {
        public static LoggingFacility UseCastleLog4Net(this LoggingFacility loggingFacility)
        {
            return loggingFacility.LogUsing<Log4NetLoggerFactory>();
        }
    }
}
