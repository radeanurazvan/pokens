using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnsureThat;
using Microsoft.EntityFrameworkCore;
using Pomelo.Kernel.Domain;
using Pomelo.Kernel.Messaging.Abstractions;

namespace Pomelo.Kernel.EntityFramework
{
    internal sealed class EntityFrameworkWriteRepository<T> : IWriteRepository<T>
        where T : AggregateRoot
    {
        private readonly DbContext context;
        private readonly IEventStore eventStore;
        private readonly IMessageBus messageBus;

        public EntityFrameworkWriteRepository(DbContext context, IEventStore eventStore, IMessageBus messageBus)
        {
            EnsureArg.IsNotNull(context);
            EnsureArg.IsNotNull(eventStore);
            EnsureArg.IsNotNull(messageBus);
            this.context = context;
            this.eventStore = eventStore;
            this.messageBus = messageBus;
        }

        public async Task Add(T aggregate)
        {
            await this.context.Set<T>().AddAsync(aggregate);
        }

        public async Task Save()
        {
            await this.context.SaveChangesAsync();
            await this.ProcessEvents();
        }

        private async Task ProcessEvents()
        {
            var aggregatesWithEvents = this.context.ChangeTracker.Entries()
                .Where(e => e.Entity is IAggregateRoot)
                .Select(e => e.Entity as IAggregateRoot)
                .Where(a => a.Events.Any())
                .ToList();

            await eventStore.StoreEventsFor(aggregatesWithEvents);
            await PublishEventsFor(aggregatesWithEvents);
        }

        private Task PublishEventsFor(IEnumerable<IAggregateRoot> aggregates)
        {
            var publishTasks = aggregates.SelectMany(a =>
            {
                var metadata = new IntegrationEventMetadata(a.GetId());
                return a.Events.Select(e =>
                {
                    var integrationEventType = typeof(IntegrationEvent<>).MakeGenericType(e.GetType());
                    var integrationEvent = Activator.CreateInstance(integrationEventType, e, metadata);

                    var publishMethod = typeof(IMessageBus).GetMethod(nameof(IMessageBus.Publish))
                        .MakeGenericMethod(integrationEventType);
                    return (Task) publishMethod.Invoke(this.messageBus, new[] {integrationEvent});
                });
            });

            return Task.WhenAll(publishTasks);
        }
    }
}