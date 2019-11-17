using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;

namespace Pomelo.Kernel.DataSynchronization
{
    public interface ISyncReadRepository<T>
        where T : SynchronizationModel
    {
        Task<Maybe<T>> GetById(string id);

        Task<IEnumerable<T>> GetAll();

        Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate);
    }
}