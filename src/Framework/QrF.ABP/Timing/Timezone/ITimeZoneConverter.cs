using System;
using System.Collections.Generic;
using System.Text;

namespace QrF.ABP.Timing.Timezone
{
    /// <summary>
    /// Interface for timezone converter
    /// </summary>
    public interface ITimeZoneConverter
    {
        /// <summary>
        /// Converts given date to application's time zone. 
        /// If timezone setting is not specified, returns given date.
        /// </summary>
        /// <param name="date">Base date to convert</param>
        /// <returns></returns>
        DateTime? Convert(DateTime? date);
    }
}
