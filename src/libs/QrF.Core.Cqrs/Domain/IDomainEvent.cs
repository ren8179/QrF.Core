using QrF.Core.Cqrs.Events;
using System;

namespace QrF.Core.Cqrs.Domain
{
    public interface IDomainEvent<TKey> : IEvent
    {
        TKey Id { get; set; }
        TKey AggregateRootId { get; set; }
        TKey CommandId { get; set; }
        string UserId { get; set; }
        string Source { get; set; }
        DateTime TimeStamp { get; set; }
        void Update(IDomainCommand<TKey> command);
    }
}
