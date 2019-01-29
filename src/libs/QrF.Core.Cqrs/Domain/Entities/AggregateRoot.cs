using ReflectionMagic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace QrF.Core.Cqrs.Domain.Entities
{
    public abstract class AggregateRoot<TKey> : IAggregateRoot
    {
        [Key]
        public Guid Id { get; protected set; }
        public int Version { get; private set; }

        private readonly List<IDomainEvent<TKey>> _events = new List<IDomainEvent<TKey>>();
        public ReadOnlyCollection<IDomainEvent<TKey>> Events => _events.AsReadOnly();

        protected AggregateRoot()
        {
            Id = Guid.NewGuid();
        }

        protected AggregateRoot(Guid id)
        {
            if (id == Guid.Empty)
                id = Guid.NewGuid();

            Id = id;
        }

        public void LoadsFromHistory(IEnumerable<IDomainEvent<TKey>> events)
        {
            var domainEvents = events as IDomainEvent<TKey>[] ?? events.ToArray();

            foreach (var @event in domainEvents)
                this.AsDynamic().Apply(@event);

            Version = domainEvents.Length;
        }

        /// <summary>
        /// Adds the event to the new events collection.
        /// </summary>
        /// <param name="event">The event.</param>
        protected void AddEvent(IDomainEvent<TKey> @event)
        {
            _events.Add(@event);
        }

        /// <summary>
        /// Adds the event to the new events collection and calls the related apply method.
        /// </summary>
        /// <param name="event">The event.</param>
        protected void AddAndApplyEvent(IDomainEvent<TKey> @event)
        {
            _events.Add(@event);
            this.AsDynamic().Apply(@event);
        }
    }
}
