using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pomelo.Kernel.Domain
{
    public interface IEventStore
    {
        Task StoreEventsFor(IEnumerable<IAggregateRoot> aggregates);
    }
}