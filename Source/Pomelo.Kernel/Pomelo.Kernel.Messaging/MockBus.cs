using System.Threading.Tasks;
using EnsureThat;
using MediatR;
using Pomelo.Kernel.Messaging.Abstractions;

namespace Pomelo.Kernel.Messaging
{
    internal sealed class MockBus : IMessageBus
    {
        private readonly IMediator mediator;

        public MockBus(IMediator mediator)
        {
            EnsureArg.IsNotNull(mediator);
            this.mediator = mediator;
        }

        public Task Publish<T>(T message) where T : IBusMessage
        {
            return this.mediator.Publish(message);
        }
    }
}