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
        private readonly MongoContext context;
        private readonly IEventStore eventStore;

        public MongoCollectionRepository(MongoSettings settings, MongoContext context, IEventStore eventStore)
        {
            var client = new MongoClient(settings.ConnectionString);
            this.database = client.GetDatabase(settings.Database);
            this.context = context;
            this.eventStore = eventStore;
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
            where T : DocumentAggregate
        {
            var aggregate = await GetCollection<T>().Find(predicate).FirstOrDefaultAsync();
            if (aggregate != null)
            {
                context.Attach(aggregate);
            }
            return aggregate;
        }

        public Task Add<T>(T aggregate)
            where T : DocumentAggregate
        {
            context.Attach(aggregate);
            return GetCollection<T>().InsertOneAsync(aggregate);
        }

        public Task Update<T>(T aggregate)
            where T : DocumentAggregate
        {
            context.Attach(aggregate);
            return GetCollection<T>().ReplaceOneAsync(a => a.Id == aggregate.Id, aggregate);
        }

        public Task Delete<T>(T aggregate) 
            where T : DocumentAggregate
        {
            return GetCollection<T>().FindOneAndDeleteAsync(x => x.Id == aggregate.Id);
        }

        public Task Commit()
        {
            return this.eventStore.StoreEventsFor(this.context.AttachedAggregates);
        }

        private IMongoCollection<T> GetCollection<T>() => database.GetCollection<T>(typeof(T).Name);
    }
}