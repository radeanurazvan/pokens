using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using CSharpFunctionalExtensions;

namespace Pokens.Pokedex.Domain
{
    public interface IPokedexRepository
    {
        IEnumerable<T> GetAll<T>();

        IEnumerable<T> Find<T>(Expression<Func<T, bool>> predicate);

        Maybe<T> FindOne<T>(Expression<Func<T, bool>> predicate);

        void Add<T>(T aggregate);
        
        void Update<T>(T aggregate)
            where T : PokedexEntity;
    }
}