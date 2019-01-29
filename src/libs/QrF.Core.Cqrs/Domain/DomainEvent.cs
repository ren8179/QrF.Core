﻿using System;
using System.Collections.Generic;
using System.Text;

namespace QrF.Core.Cqrs.Domain
{
    public abstract class DomainEvent : IDomainEvent<Guid>
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid AggregateRootId { get; set; }
        public Guid CommandId { get; set; }
        public string UserId { get; set; }
        public string Source { get; set; }
        public DateTime TimeStamp { get; set; } = DateTime.UtcNow;

        public void Update(IDomainCommand<Guid> command)
        {
            CommandId = command.Id;
            UserId = command.UserId;
            Source = command.Source;
        }
    }
}
