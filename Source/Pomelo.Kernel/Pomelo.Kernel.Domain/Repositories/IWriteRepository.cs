using System.Threading.Tasks;

namespace Pomelo.Kernel.Domain
{
    public interface IWriteRepository<T>
        where T : AggregateRoot
    {
        Task Add(T aggregate);

        Task Save();
    }
}