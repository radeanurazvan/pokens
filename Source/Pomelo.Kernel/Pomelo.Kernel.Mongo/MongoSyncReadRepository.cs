using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using MongoDB.Driver;
using Pomelo.Kernel.DataSynchronization;

namespace Pomelo.Kernel.Mongo
{
    internal sealed class MongoSyncReadRepository<T> : ISyncReadRepository<T>
        where T : SynchronizationModel, new()
    {
        private readonly IMongoCollection<T> collection;

        public MongoSyncReadRepository(MongoClient client, MongoSettings settings)
        {
            this.collection = client.GetDatabase(settings.Database)
                .GetCollection<T>(new T().GetCollectionName());
        }

        public async Task<Maybe<T>> GetById(string id)
        {
            return await this.collection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await this.collection.AsQueryable().ToListAsync();
        }

        public async Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate)
        {
            try
            {
                var filter = new FilterDefinitionBuilder<T>().Where(predicate);
                return await this.collection.Find(filter).ToListAsync();
            }

            catch
            {
                return this.collection.AsQueryable().Where(predicate.Compile()).AsEnumerable();
            }
        }
    }
}