using System;
using System.Threading.Tasks;
using EnsureThat;
using MongoDB.Driver;
using Pomelo.Kernel.DataSynchronization;

namespace Pomelo.Kernel.Mongo
{
    internal sealed class MongoSyncStorage : ISyncStorage
    {
        private readonly MongoSettings settings;
        private readonly MongoClient client;

        public MongoSyncStorage(MongoSettings settings, MongoClient client)
        {
            EnsureArg.IsNotNull(client);
            this.settings = settings;
            this.client = client;
        }

        public Task Create<T>(T entity) where T : SynchronizationModel, new()
        {
            return GetCollection<T>()
                .InsertOneAsync(entity);
        }

        public async Task Update<T>(string id, Action<T> update) where T : SynchronizationModel, new()
        {
            var collection = GetCollection<T>();
            var replacement = await (await collection.FindAsync(x => x.Id == id)).FirstOrDefaultAsync();

            if (replacement == null)
            {
                return;
            }

            update(replacement);
            await collection.ReplaceOneAsync(x => x.Id == id, replacement);
        }

        public Task Delete<T>(string id) where T : SynchronizationModel, new()
        {
            return GetCollection<T>().FindOneAndDeleteAsync(x => x.Id == id);
        }

        private IMongoCollection<T> GetCollection<T>()
            where T : SynchronizationModel, new()
        {
            return  this.client.GetDatabase(this.settings.Database)
                .GetCollection<T>(new T().GetCollectionName());
        }
    }
}