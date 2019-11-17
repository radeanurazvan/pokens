using System;
using System.Text;
using EventStore.ClientAPI;
using Newtonsoft.Json;

namespace Pomelo.Kernel.EventStore
{
    internal sealed class CheckpointRegistered
    {
        public CheckpointRegistered(Position lastProcessedPosition)
        {
            LastProcessedPosition = lastProcessedPosition;
        }

        public Position LastProcessedPosition { get; private set; }

        public EventData ToEventData()
        {
            var serializedData = JsonConvert.SerializeObject(this);
            var eventType = $"{this.GetType().Name}, {this.GetType().Assembly.GetName().Name}";

            return new EventData(Guid.NewGuid(), eventType, true, Encoding.UTF8.GetBytes(serializedData), new byte[] { });
        }
    }

}