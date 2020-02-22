using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Pokens.Battles.Domain;
using Pomelo.Kernel.Events.Abstractions;

namespace Pokens.Battles.Business
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBattlesBusiness(this IServiceCollection services)
        {
            return services.AddEventHandlers()
                .AddMediatR(typeof(ServiceCollectionExtensions).Assembly);
        }
    }
}