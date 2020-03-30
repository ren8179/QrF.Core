using System;
using System.Collections.Generic;
using System.Text;

namespace QrF.Core.Utils.DelayQueues
{
    public static class NotifyPlanManager
    {
        private static DelayQueue<NotifyPlan> _queue = new DelayQueue<NotifyPlan>(60);

        public static void Insert(NotifyPlan plan, DateTime time)
        {
            _queue.Insert(plan, time);
        }

        public static void Read()
        {
            _queue.Read();
        }
    }
}
