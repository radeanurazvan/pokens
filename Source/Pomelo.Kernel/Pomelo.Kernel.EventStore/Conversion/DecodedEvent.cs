using System;
using System.Text;
using EventStore.ClientAPI;
using Newtonsoft.Json;

namespace Pomelo.Kernel.EventStore
{
    internal sealed class DecodedEvent
    {
        public DecodedEvent(RecordedEvent @event)
        {
            var serializedMetadata = Encoding.UTF8.GetString(@event.Metadata);
            var metadata = JsonConvert.DeserializeObject<EventMetadata>(serializedMetadata);

            var eventBody = Encoding.UTF8.GetString(@event.Data);

            Metadata = metadata;
            Value = JsonConvert.DeserializeObject(eventBody, Type.GetType(metadata.EventType));
        }

        public EventMetadata Metadata { get; }

        public object Value { get; }
    }
}