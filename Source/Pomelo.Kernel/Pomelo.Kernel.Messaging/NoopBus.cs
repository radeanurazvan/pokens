using System.Threading.Tasks;
using Pomelo.Kernel.Messaging.Abstractions;

namespace Pomelo.Kernel.Messaging
{
    internal sealed class NoopBus : IMessageBus
    {
        public Task Publish<T>(T message) where T : IBusMessage
        {
            return Task.CompletedTask;
        }
    }
}