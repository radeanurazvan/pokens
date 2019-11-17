using System;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;

namespace Pomelo.Kernel.Domain
{
    public interface IGetById<T>
        where T : AggregateRoot
    {
        Task<Maybe<T>> GetById(Guid id);
    }
}