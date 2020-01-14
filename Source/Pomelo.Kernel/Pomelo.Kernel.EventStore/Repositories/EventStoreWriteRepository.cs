using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnsureThat;
using EventStore.ClientAPI;
using Newtonsoft.Json;
using Pomelo.Kernel.Domain;

namespace Pomelo.Kernel.EventStore
{
    internal sealed class EventStoreWriteRepository<T> : IWriteRepository<T>
        where T : AggregateRoot
    {
        private readonly IEventStoreConnection connection;
        private readonly IServiceProvider provider;
        private readonly EventStoreContext context;

        public EventStoreWriteRepository(IEventStoreConnection connection, IServiceProvider provider, EventStoreContext context)
        {
            EnsureArg.IsNotNull(connection);
            EnsureArg.IsNotNull(provider);
            EnsureArg.IsNotNull(context);
            this.connection = connection;
            this.provider = provider;
            this.context = context;
        }

        public Task Add(T aggregate)
        {
            return Task.Run(() => context.Attach(aggregate));
        }

        public Task Save()
        {
            var tasks = this.context.AttachedAggregates.Select(a =>
            {
                var events = a.Events.Select(e => CreateEventData(e, a.Id));
                var streamConfigType = typeof(IStreamConfig<>).MakeGenericType(a.GetType());
                dynamic streamConfig = provider.GetService(streamConfigType);

                return connection.AppendToStreamAsync(streamConfig.GetStreamFor(a.Id), ExpectedVersion.Any, events);
            });
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