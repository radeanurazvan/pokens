namespace Pomelo.Kernel.Domain
{
    public interface IRepositoryMediator
    {
        IReadRepository<T> Read<T>()
            where T : AggregateRoot;

        IWriteRepository<T> Write<T>()
            where T : AggregateRoot;
    }
}