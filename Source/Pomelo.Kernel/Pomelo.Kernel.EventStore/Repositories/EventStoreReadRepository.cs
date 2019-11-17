using System;
using System.Linq;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using EnsureThat;
using EventStore.ClientAPI;
using Pomelo.Kernel.Common;
using Pomelo.Kernel.Domain;

namespace Pomelo.Kernel.EventStore
{
    internal sealed class EventStoreReadRepository<T> : IGetById<T>
        where T : AggregateRoot
    {
        private readonly IEventStoreConnection connection;
        private readonly IStreamConfig<T> streamConfig;
        private readonly EventStoreContext context;

        private readonly int sliceReadSize = 100;

        public EventStoreReadRepository(IEventStoreConnection connection, IStreamConfig<T> streamConfig, EventStoreContext context)
        {
            EnsureArg.IsNotNull(connection);
            EnsureArg.IsNotNull(streamConfig);
            EnsureArg.IsNotNull(context);
            this.connection = connection;
            this.streamConfig = streamConfig;
            this.context = context;
        }

        public Task<Maybe<T>> GetById(Guid id)
        {
            return context.GetById<T>(id)
                .OnFailureCompensate(() => GetFromStore(id))
                .Ensure(a => !a.IsDeleted, "Entity is deleted")
                .ToMaybe();
        }

        private async Task<Result<T>> GetFromStore(Guid id)
        {
            var aggregate = (T)Activator.CreateInstance(typeof(T), true);

            var stream = this.streamConfig.GetStreamFor(id);
            var sliceStart = 0L;

            var sliceResult = await Result.Ok()
                .Map(() => connection.ReadStreamEventsForwardAsync(stream, sliceStart, sliceReadSize, false));
            do
            {
                sliceResult.Ensure(sr => sr.Status != SliceReadStatus.StreamNotFound && sr.Status != SliceReadStatus.StreamDeleted, "Stream not found")
                    .Tap(sr =>
                    {
                        var decodedEvents = sr.Events.Select(resolvedEvent => new DecodedEvent(resolvedEvent.Event));
                        foreach (var decodedEvent in decodedEvents)
                        {
                            aggregate.Mutate(decodedEvent.Value as IDomainEvent);
                        }
                    });

            } while(!sliceResult.IsSuccess && !sliceResult.Value.IsEndOfStream);

            return sliceResult
                .Map(_ => aggregate)
                .Tap(a => context.Attach(a));
        }
    }
}