using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Pomelo.Kernel.Events.Abstractions;

namespace Pokens.Training.Business
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddTrainingBusiness(this IServiceCollection services)
        {
            return services
                .AddMediatR(typeof(ServiceCollectionExtensions).Assembly)
                .AddEventHandlers(typeof(TrainerCreatedEvent).Assembly);
        }
    }
}