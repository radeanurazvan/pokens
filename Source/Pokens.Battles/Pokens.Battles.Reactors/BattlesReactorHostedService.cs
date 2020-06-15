using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventStore.ClientAPI;
using MediatR;
using Microsoft.Extensions.Hosting;
using Pokens.Battles.Domain;
using Pomelo.Kernel.Common;
using Pomelo.Kernel.Domain;
using Pomelo.Kernel.EventStore;
using Pomelo.Kernel.EventStore.Subscriptions;

namespace Pokens.Battles.Reactors
{
    internal sealed class BattlesReactorHostedService : IHostedService
    {
        private const string BattlesTag = nameof(Battles);
        private readonly IEventStoreSubscriptionBuilder subscriptionBuilder;
        private readonly IMediator mediator;

        public BattlesReactorHostedService(IEventStoreSubscriptionBuilder subscriptionBuilder, IMediator mediator)
        {
            this.subscriptionBuilder = subscriptionBuilder;
            this.mediator = mediator;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Battle reactor is up..");
            await Task.Delay(TimeSpan.FromSeconds(5), cancellationToken);

            var subscriptions = DomainAssembly.Value.GetTypes()
                .Where(t => !t.IsAbstract && typeof(AggregateRoot).IsAssignableFrom(t))
                .Select(a => this.subscriptionBuilder.Persistent()
                    .WithGroup("catalog-reactors")
                    .ForStream($"$ce-{BattlesTag}{a.GetFriendlyName()}")
                    .WithCheckpointTag($"{BattlesTag}{a.GetFriendlyName()}")
                    .WithConsumer(PublishNotification)
                    .Build());

            foreach (var subscription in subscriptions)
            {
                await subscription.Create();
                await subscription.Connect();
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        private Task PublishNotification(EventStorePersistentSubscriptionBase subscription, ResolvedEvent @event)
        {
            var notification = @event.Event.ToNotificationEvent();
            var genericPublish = typeof(IMediator)
                .GetMethods()
                .First(m => m.Name == nameof(IMediator.Publish) && m.IsGenericMethod)
                .MakeGenericMethod(notification.GetType());

            return (Task)genericPublish.Invoke(this.mediator, new[] { notification, CancellationToken.None });
        }
    }
}