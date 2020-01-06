using System;
using EventStore.ClientAPI;
using Polly;

namespace Pomelo.Kernel.EventStore
{
    internal sealed class EventStoreConnectionFactory
    {
        private readonly EventStoreSettings settings;
        private readonly EventStoreEvents events;

        public EventStoreConnectionFactory(EventStoreSettings settings, EventStoreEvents events)
        {
            this.settings = settings;
            this.events = events;
        }

        public IEventStoreConnection CreateConnection()
        {
            return Policy.Handle<Exception>()
                .WaitAndRetry(3, attempt => TimeSpan.FromSeconds(attempt), (e, _) => events.RaiseConnectionFailure(e))
                .ExecuteAndCapture(() =>
                {
                    var eventStoreConnection = EventStoreConnection.Create(settings.ConnectionString);
                    eventStoreConnection.ConnectAsync().GetAwaiter().GetResult();

                    return eventStoreConnection;
                }).Result;
        }
    }
}