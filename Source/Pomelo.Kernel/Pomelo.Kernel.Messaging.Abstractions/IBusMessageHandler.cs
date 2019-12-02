using System.Threading.Tasks;

namespace Pomelo.Kernel.Messaging.Abstractions
{
    public interface IBusMessageHandler<in T>
        where T : IBusMessage
    {
        Task Handle(T message);
    }
}