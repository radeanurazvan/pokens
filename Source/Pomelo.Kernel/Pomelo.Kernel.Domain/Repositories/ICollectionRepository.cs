using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;

namespace Pomelo.Kernel.Domain
{
    public interface ICollectionRepository
    {
        Task<IEnumerable<T>> GetAll<T>();

        Task<IEnumerable<T>> Find<T>(Expression<Func<T, bool>> predicate);

        Task<Maybe<T>> FindOne<T>(Expression<Func<T, bool>> predicate)
            where T : DocumentAggregate;

        Task Add<T>(T aggregate)
            where T : DocumentAggregate;
        
        Task Update<T>(T aggregate)
            where T : DocumentAggregate;

        Task Delete<T>(T aggregate)
            where T : DocumentAggregate;

        Task Commit();
    }
}