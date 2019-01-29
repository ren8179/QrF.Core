using QrF.Core.Cqrs.Commands;
using System;

namespace QrF.Core.Cqrs.Domain
{
    public interface IDomainCommand<TKey> : ICommand
    {
        TKey Id { get; set; }
        TKey AggregateRootId { get; set; }
        string UserId { get; set; }
        string Source { get; set; }
        DateTime TimeStamp { get; set; }
        int? ExpectedVersion { get; set; }
    }
}
