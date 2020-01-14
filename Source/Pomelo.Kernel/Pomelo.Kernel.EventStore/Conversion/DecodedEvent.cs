using System;
using System.Text;
using EventStore.ClientAPI;
using Newtonsoft.Json;
using Pomelo.Kernel.Infrastructure;

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
            Value = JsonConvert.DeserializeObject(eventBody, Type.GetType(metadata.EventType), new JsonSerializerSettings
            {
                ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
                ContractResolver = new PrivateCamelcaseResolver()
            });
        }

        public EventMetadata Metadata { get; }

        public object Value { get; }
    }
}