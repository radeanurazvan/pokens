using System.Threading.Tasks;

namespace Pomelo.Kernel.Messaging.Abstractions
{
    public interface IMessageBus
    {
        Task Publish<T>(T message)
            where T : IBusMessage;
    }
}