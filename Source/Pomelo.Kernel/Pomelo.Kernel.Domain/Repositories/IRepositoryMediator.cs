using System;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;

namespace Pomelo.Kernel.Domain
{
    public interface IRepositoryMediator
    {
        IReadRepository<T> Read<T>()
            where T : AggregateRoot;

        Task<Maybe<T>> ReadById<T>(Guid id)
            where T : AggregateRoot;

        IWriteRepository<T> Write<T>()
            where T : AggregateRoot;
    }
}