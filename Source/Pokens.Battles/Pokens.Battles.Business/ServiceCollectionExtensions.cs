using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Pomelo.Kernel.Events.Abstractions;

namespace Pokens.Battles.Business
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBattlesBusiness(this IServiceCollection services)
        {
            var assembly = typeof(ServiceCollectionExtensions).Assembly;
            return services.AddEventHandlers(assembly)
                .AddMediatR(assembly);
        }
    }
}