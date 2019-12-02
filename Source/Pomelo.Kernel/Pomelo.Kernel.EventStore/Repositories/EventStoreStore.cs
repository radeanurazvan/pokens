using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnsureThat;
using EventStore.ClientAPI;
using Newtonsoft.Json;
using Pomelo.Kernel.Domain;

namespace Pomelo.Kernel.EventStore
{
    internal sealed class EventStoreStore : IEventStore
    {
        private readonly IEventStoreConnection connection;
        private readonly IServiceProvider provider;

        public EventStoreStore(IEventStoreConnection connection, IServiceProvider provider)
        {
            EnsureArg.IsNotNull(connection);
            EnsureArg.IsNotNull(provider);
            this.connection = connection;
            this.provider = provider;
        }

        public Task StoreEventsFor(IEnumerable<AggregateRoot> aggregates)
        {
            var aggregatesByType = aggregates.ToLookup(a => a.GetType());
            var tasks = aggregatesByType.SelectMany(x =>
            {
                dynamic streamConfig = this.provider.GetService(typeof(IStreamConfig<>).MakeGenericType(x.Key));
                return x.Select(a =>
                {
                    var aggregateEvents = a.Events.ToList();
                    //a.ClearEvents();

                    var events = aggregateEvents.Select(e => CreateEventData(e, a.Id));
                    return connection.AppendToStreamAsync(streamConfig.GetStreamFor(a.Id), ExpectedVersion.Any, events);
                });
            }).ToList();

            return Task.WhenAll(tasks.Cast<Task>());
        }


        private EventData CreateEventData(IDomainEvent @event, Guid aggregateId)
        {
            var metadata = new EventMetadata(aggregateId, @event.GetType());
            var metadataBytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(metadata));
            var data = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(@event));

            return new EventData(Guid.NewGuid(), @event.GetType().Name, true, data, metadataBytes);
        }
    }
}