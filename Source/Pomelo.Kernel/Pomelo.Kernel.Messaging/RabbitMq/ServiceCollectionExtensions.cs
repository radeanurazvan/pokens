using Microsoft.Extensions.DependencyInjection;
using Pomelo.Kernel.Common;
using Pomelo.Kernel.Messaging.Abstractions;
using RabbitMQ.Client;

namespace Pomelo.Kernel.Messaging
{
    public static partial class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPomeloRabbitMqBus(this IServiceCollection services)
        {
            return services.AddSingletonSettings<RabbitMqSettings>()
                .AddRabbitMqConnection()
                .AddRabbitMqModel()
                .AddSingleton<IMessageBus, RabbitMqBus>();
        }

        private static IServiceCollection AddRabbitMqConnection(this IServiceCollection services)
        {
            return services.AddSingleton(p =>
            {
                var settings = p.GetService<RabbitMqSettings>();
                return new ConnectionFactory
                {
                    HostName = settings.Server,
                    Port = settings.Port,
                    UserName = settings.Username,
                    Password = settings.Password
                }.CreateConnection();
            });
        }

        private static IServiceCollection AddRabbitMqModel(this IServiceCollection services)
        {
            return services.AddSingleton(p => p.GetService<IConnection>().CreateModel());
        }
    }
}