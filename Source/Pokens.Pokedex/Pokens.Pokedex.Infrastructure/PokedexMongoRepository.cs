using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using CSharpFunctionalExtensions;
using MongoDB.Driver;
using Pokens.Pokedex.Domain;

namespace Pokens.Pokedex.Infrastructure
{
    internal sealed class PokedexMongoRepository : IPokedexRepository
    {
        private readonly IMongoDatabase database;

        public PokedexMongoRepository(PokedexMongoSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            this.database = client.GetDatabase(settings.Database);
        }

        public IEnumerable<T> GetAll<T>()
        {
            return GetCollection<T>().AsQueryable().ToList();
        }

        public IEnumerable<T> Find<T>(Expression<Func<T, bool>> predicate)
        {
            return GetCollection<T>().Find(predicate).ToList();
        }

        public Maybe<T> FindOne<T>(Expression<Func<T, bool>> predicate)
        {
            return GetCollection<T>().Find(predicate).FirstOrDefault();
        }

        public void Add<T>(T aggregate)
        {
            GetCollection<T>().InsertOne(aggregate);
        }

        public void Update<T>(T aggregate)
            where T : PokedexEntity
        {
            GetCollection<T>().ReplaceOne(a => a.Id == aggregate.Id, aggregate);
        }

        private IMongoCollection<T> GetCollection<T>() => database.GetCollection<T>(typeof(T).Name);
    }
}