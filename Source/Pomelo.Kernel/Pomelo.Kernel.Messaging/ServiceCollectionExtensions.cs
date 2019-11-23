using Microsoft.Extensions.DependencyInjection;
using Pomelo.Kernel.Messaging.Abstractions;

namespace Pomelo.Kernel.Messaging
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPomeloMockMessaging(this IServiceCollection services)
        {
            return services.AddScoped<IMessageBus, MockBus>();
        }

        public static IServiceCollection AddPomeloNoopMessaging(this IServiceCollection services)
        {
            return services.AddScoped<IMessageBus, NoopBus>();
        }
    }
}