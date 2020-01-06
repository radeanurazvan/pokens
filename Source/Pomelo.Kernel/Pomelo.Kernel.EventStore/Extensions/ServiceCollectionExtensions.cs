using Microsoft.Extensions.DependencyInjection;
using Pomelo.Kernel.Common;
using Pomelo.Kernel.Domain;

namespace Pomelo.Kernel.EventStore
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPomeloEventStore(this IServiceCollection services)
        {
            return services
                .AddSingleton<EventStoreEvents>()
                .AddSingletonSettings<EventStoreSettings>()
                .AddScoped<EventStoreContext>()
                .AddSingleton(typeof(IStreamConfig<>), typeof(DefaultStreamConfig<>))
                .AddScoped<IEventStore, EventStoreStore>()
                .AddSingleton<EventStoreConnectionFactory>()
                .AddSingleton(p => p.GetService<EventStoreConnectionFactory>().CreateConnection());
        }

        public static IServiceCollection AddPomeloEventSourcedRepositories(this IServiceCollection services)
        {
            return services.AddScoped(typeof(IGetById<>), typeof(EventStoreReadRepository<>))
                .AddScoped(typeof(IWriteRepository<>), typeof(EventStoreWriteRepository<>));
        }
    }
}