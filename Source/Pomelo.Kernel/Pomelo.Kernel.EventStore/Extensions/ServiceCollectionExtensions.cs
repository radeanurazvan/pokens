using EventStore.ClientAPI;
using Microsoft.Extensions.DependencyInjection;
using Pomelo.Kernel.Common;
using Pomelo.Kernel.Domain;

namespace Pomelo.Kernel.EventStore
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddEventStore(this IServiceCollection services)
        {
            return services
                .AddScoped<EventStoreContext>()
                .AddSingleton(typeof(IStreamConfig<>), typeof(DefaultStreamConfig<>))
                .AddSingletonSettings<EventStoreSettings>()
                .AddScoped<IEventStore, EventStoreStore>()
                .AddSingleton(ctx =>
                {
                    var settings = ctx.GetService<EventStoreSettings>();

                    var eventStoreConnection = EventStoreConnection.Create(settings.ConnectionString);
                    eventStoreConnection.ConnectAsync().Wait();

                    return eventStoreConnection;
                });
        }

        public static IServiceCollection AddEventSourcedRepositories(this IServiceCollection services)
        {
            return services.AddScoped(typeof(IGetById<>), typeof(EventStoreReadRepository<>))
                .AddScoped(typeof(IWriteRepository<>), typeof(EventStoreWriteRepository<>));
        }
    }
}