using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Pomelo.Kernel.Messaging.Abstractions;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Newtonsoft.Json;
using Polly;

namespace Pomelo.Kernel.Messaging
{
    internal sealed class RabbitMqBus : IMessageBus
    {
        private readonly IModel model;
        private readonly IServiceProvider provider;

        public RabbitMqBus(IModel model, IServiceProvider provider)
        {
            this.model = model;
            this.provider = provider;
        }

        public Task Publish<T>(T message) where T : IBusMessage
        {
            var exchangeName = typeof(T).GetExchangeName();

            model.ExchangeDeclare(exchangeName, ExchangeType.Fanout);
            var body = Encoding.UTF8.GetBytes(message.ToJson());

            var properties = model.CreateBasicProperties();
            properties.Persistent = true;
            model.BasicPublish(exchangeName, string.Empty, properties, body);

            return Task.CompletedTask;
        }

        public void Subscribe<T>() where T : IBusMessage
        {
            using var scope = provider.CreateScope();
            var handlers = scope.ServiceProvider.GetServices<IBusMessageHandler<T>>();
            foreach (var handler in handlers)
            {
                SubscribeHandler<T>(handler.GetType());
            }
        }

        private void SubscribeHandler<T>(Type handler) 
            where T : IBusMessage
        {
            var topic = typeof(T).GetExchangeName();

            model.ExchangeDeclare(topic, ExchangeType.Fanout);
            var queueName = model.QueueDeclare(handler.FullName, true, false, false).QueueName;
            model.QueueBind(queueName, topic, handler.FullName);

            var consumer = new EventingBasicConsumer(model);
            consumer.Received += Consume<T>(handler);

            model.BasicConsume(queueName, true, consumer);
        }

        private EventHandler<BasicDeliverEventArgs> Consume<T>(Type handlerType)
            where T : IBusMessage
        {
            return async (_, args) =>
            {
                var jsonEvent = JsonConvert.DeserializeObject<T>(args.Body.ToUtf8String());

                using var scope = provider.CreateScope();
                var handler = scope.ServiceProvider.GetService(handlerType) as IBusMessageHandler<T>;
                if (handler == null)
                {
                    throw new InvalidOperationException($"No RabbitMQ handler {handlerType}");
                }

                await Policy.Handle<Exception>()
                    .WaitAndRetryAsync(5, x => TimeSpan.FromSeconds(2 * x))
                    .ExecuteAsync(() => handler.Handle(jsonEvent));
            };
        }
    }

    internal static class BusMessageExtensions
    {
        public static string GetExchangeName<T>(this T message)
        {
            return message.GetType().GetExchangeName();
        }

        public static string GetExchangeName(this Type messageType)
        {
            return messageType.GetFriendlyName();
        }

        private static string GetFriendlyName(this Type type)
        {
            var friendlyName = type.Name;
            if (!type.IsGenericType)
            {
                return friendlyName;
            }

            var iBacktick = friendlyName.IndexOf('`');
            if (iBacktick > 0)
            {
                friendlyName = friendlyName.Remove(iBacktick);
            }
            friendlyName += "<";
            var typeParameters = type.GetGenericArguments();
            for (var i = 0; i < typeParameters.Length; ++i)
            {
                var typeParamName = GetFriendlyName(typeParameters[i]);
                friendlyName += (i == 0 ? typeParamName : "," + typeParamName);
            }
            friendlyName += ">";

            return friendlyName;
        }

        public static string ToJson(this IBusMessage message)
        {
            return JsonConvert.SerializeObject(message);
        }

        public static string ToUtf8String(this byte[] bytes)
        {
            return Encoding.UTF8.GetString(bytes);
        }
    }
}