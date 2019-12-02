using System.Threading.Tasks;

namespace Pomelo.Kernel.Messaging.Abstractions
{
    public interface IMessageBus
    {
        Task Publish<T>(T message) where T : IBusMessage;

        void Subscribe<T>() where T : IBusMessage;
    }
}