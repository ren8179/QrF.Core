using System;
using System.Collections.Generic;
using System.Text;

namespace QrF.ABP.Timing
{
    public static class ClockProviders
    {
        public static UnspecifiedClockProvider Unspecified { get; } = new UnspecifiedClockProvider();

        public static LocalClockProvider Local { get; } = new LocalClockProvider();

        public static UtcClockProvider Utc { get; } = new UtcClockProvider();
    }
}
