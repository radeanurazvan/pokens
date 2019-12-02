using Microsoft.Extensions.DependencyInjection;
using Pomelo.Kernel.Messaging.Abstractions;

namespace Pomelo.Kernel.Messaging
{
    public static partial class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPomeloMockMessaging(this IServiceCollection services)
        {
            return services.AddScoped<IMessageBus, MockBus>();
        }
    }
}