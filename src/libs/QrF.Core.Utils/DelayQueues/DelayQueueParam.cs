using System;
using System.Collections.Generic;
using System.Text;

namespace QrF.Core.Utils.DelayQueues
{
    public class DelayQueueParam
    {
        internal int Slot { get; set; }

        internal int CycleNum { get; set; }

        public Action<object> Callback { get; set; }
    }
}
