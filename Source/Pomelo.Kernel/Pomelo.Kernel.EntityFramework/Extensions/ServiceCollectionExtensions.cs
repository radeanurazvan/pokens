using Microsoft.Extensions.DependencyInjection;
using Pomelo.Kernel.Domain;

namespace Pomelo.Kernel.EntityFramework
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPomeloEntityFrameworkRepositories(this IServiceCollection services)
        {
            return services.AddScoped(typeof(IReadRepository<>), typeof(EntityFrameworkReadRepository<>))
                .AddScoped(typeof(IWriteRepository<>), typeof(EntityFrameworkWriteRepository<>));
        }
    }
}