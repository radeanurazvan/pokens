using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;

namespace Pomelo.Kernel.Domain
{
    public interface IReadRepository<T>
        where T : AggregateRoot
    {
        Task<IEnumerable<T>> GetAll();

        Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate);

        Task<Maybe<T>> FindOne(Expression<Func<T, bool>> predicate);

        Task<Maybe<T>> GetById(Guid id);
    }
}