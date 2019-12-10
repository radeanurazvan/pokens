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

        Task<Maybe<T>> FindOne<T>(Expression<Func<T, bool>> predicate);

        Task Add<T>(T aggregate);
        
        Task Update<T>(T aggregate)
            where T : DocumentEntity;

        Task Delete<T>(string id)
            where T : DocumentEntity;
    }
}