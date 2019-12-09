using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using MongoDB.Driver;
using Pomelo.Kernel.Domain;

namespace Pomelo.Kernel.Mongo
{
    internal sealed class MongoCollectionRepository : ICollectionRepository
    {
        private readonly IMongoDatabase database;

        public MongoCollectionRepository(MongoSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            this.database = client.GetDatabase(settings.Database);
        }

        public async Task<IEnumerable<T>> GetAll<T>()
        {
            return await GetCollection<T>().AsQueryable().ToListAsync();
        }

        public async Task<IEnumerable<T>> Find<T>(Expression<Func<T, bool>> predicate)
        {
            return await GetCollection<T>().Find(predicate).ToListAsync();
        }

        public async Task<Maybe<T>> FindOne<T>(Expression<Func<T, bool>> predicate)
        {
            return await GetCollection<T>().Find(predicate).FirstOrDefaultAsync();
        }

        public Task Add<T>(T aggregate)
        {
            return GetCollection<T>().InsertOneAsync(aggregate);
        }

        public Task Update<T>(T aggregate)
            where T : DocumentEntity
        {
            return GetCollection<T>().ReplaceOneAsync(a => a.Id == aggregate.Id, aggregate);
        }

        public Task Delete<T>(string id)
            where T : DocumentEntity
        {
            return GetCollection<T>().FindOneAndDeleteAsync(x => x.Id == id);
        }

        private IMongoCollection<T> GetCollection<T>() => database.GetCollection<T>(typeof(T).Name);
    }
}