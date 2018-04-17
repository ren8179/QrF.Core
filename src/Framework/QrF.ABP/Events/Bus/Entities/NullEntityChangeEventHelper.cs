using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QrF.ABP.Events.Bus.Entities
{
    /// <summary>
    /// Null-object implementation of <see cref="IEntityChangeEventHelper"/>.
    /// </summary>
    public class NullEntityChangeEventHelper : IEntityChangeEventHelper
    {
        /// <summary>
        /// Gets single instance of <see cref="NullEntityChangeEventHelper"/> class.
        /// </summary>
        public static NullEntityChangeEventHelper Instance { get; } = new NullEntityChangeEventHelper();

        private NullEntityChangeEventHelper()
        {

        }

        public void TriggerEntityCreatingEvent(object entity)
        {

        }

        public void TriggerEntityCreatedEventOnUowCompleted(object entity)
        {

        }

        public void TriggerEntityUpdatingEvent(object entity)
        {

        }

        public void TriggerEntityUpdatedEventOnUowCompleted(object entity)
        {

        }

        public void TriggerEntityDeletingEvent(object entity)
        {

        }

        public void TriggerEntityDeletedEventOnUowCompleted(object entity)
        {

        }

        public void TriggerEvents(EntityChangeReport changeReport)
        {

        }

        public Task TriggerEventsAsync(EntityChangeReport changeReport)
        {
            return Task.FromResult(0);
        }
    }
}
