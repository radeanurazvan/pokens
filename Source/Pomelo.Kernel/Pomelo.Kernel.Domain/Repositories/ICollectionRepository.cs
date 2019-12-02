using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using CSharpFunctionalExtensions;

namespace Pomelo.Kernel.Domain
{
    public interface ICollectionRepository
    {
        IEnumerable<T> GetAll<T>();

        IEnumerable<T> Find<T>(Expression<Func<T, bool>> predicate);

        Maybe<T> FindOne<T>(Expression<Func<T, bool>> predicate);

        void Add<T>(T aggregate);
        
        void Update<T>(T aggregate)
            where T : DocumentEntity;

        void Delete<T>(string id)
            where T : DocumentEntity;
    }
}