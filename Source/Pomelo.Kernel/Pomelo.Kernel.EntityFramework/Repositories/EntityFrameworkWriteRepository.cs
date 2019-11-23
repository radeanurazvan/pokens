using System.Linq;
using System.Threading.Tasks;
using EnsureThat;
using Microsoft.EntityFrameworkCore;
using Pomelo.Kernel.Domain;

namespace Pomelo.Kernel.EntityFramework
{
    internal sealed class EntityFrameworkWriteRepository<T> : IWriteRepository<T>
        where T : AggregateRoot
    {
        private readonly DbContext context;
        private readonly IEventStore eventStore;

        public EntityFrameworkWriteRepository(DbContext context, IEventStore eventStore)
        {
            EnsureArg.IsNotNull(context);
            EnsureArg.IsNotNull(eventStore);
            this.context = context;
            this.eventStore = eventStore;
        }

        public async Task Add(T aggregate)
        {
            await this.context.Set<T>().AddAsync(aggregate);
        }

        public async Task Save()
        {
            await this.context.SaveChangesAsync();
            await this.StoreEvents();
        }

        private Task StoreEvents()
        {
            var aggregatesWithEvents = this.context.ChangeTracker.Entries()
                .Where(e => e.Entity is AggregateRoot)
                .Select(e => e.Entity as AggregateRoot)
                .Where(a => a.Events.Any());

            return eventStore.StoreEventsFor(aggregatesWithEvents);
        }
    }
}