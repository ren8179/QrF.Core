using System;
using System.Collections.Generic;
using System.Text;

namespace QrF.Core.Utils.DelayQueues
{
    public class NotifyPlan : DelayQueueParam
    {
        public Guid CamId { get; set; }

        public int PreviousTotal { get; set; }

        public int Amount { get; set; }
    }
}
