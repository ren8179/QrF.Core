using System;
using System.Collections.Generic;
using System.Text;

namespace QrF.ABP.Events.Bus.Entities
{
    [Serializable]
    public class DomainEventEntry
    {
        public object SourceEntity { get; }

        public IEventData EventData { get; }

        public DomainEventEntry(object sourceEntity, IEventData eventData)
        {
            SourceEntity = sourceEntity;
            EventData = eventData;
        }
    }
}
