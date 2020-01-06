using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Pomelo.Kernel.EventStore;

namespace Pokens.Battles.Reactors
{
    internal sealed class BattlesReactorHostedService : IHostedService
    {
        private readonly IEnumerable<IEventStorePersistentSubscription> persistentSubscriptions;

        public BattlesReactorHostedService(IEnumerable<IEventStorePersistentSubscription> persistentSubscriptions)
        {
            this.persistentSubscriptions = persistentSubscriptions.Select(p => p.ForGroup("battles-reactor"));
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Battles reactor is up..");

            foreach (var subscription in this.persistentSubscriptions)
            {
                await subscription.Create();
                await subscription.Connect();
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}