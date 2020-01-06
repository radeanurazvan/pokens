using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Pokens.Battles.Domain;
using Pomelo.Kernel.Common;
using Pomelo.Kernel.Domain;
using Pomelo.Kernel.EventStore;

namespace Pokens.Battles.Reactors
{
    internal static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPersistentSubscriptions(this IServiceCollection services)
        {
            DomainAssembly.Value.GetTypes()
                .Where(t => !t.IsAbstract && typeof(AggregateRoot).IsAssignableFrom(t))
                .Select(a => typeof(EventStorePersistentSubscription<>).MakeGenericType(a))
                .ForEach(subscription => services.AddSingleton(typeof(IEventStorePersistentSubscription), subscription));

            return services;
        }
    }
}