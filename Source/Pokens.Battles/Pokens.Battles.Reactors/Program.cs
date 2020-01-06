using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Pomelo.Kernel.EventStore;
using Pomelo.Kernel.Infrastructure;
using Pomelo.Kernel.Mongo;

namespace Pokens.Battles.Reactors
{
    public static class Program
    {
        public static Task Main()
        {
            var builder = new HostBuilder()
                .ConfigureLogging(l => l.AddConsole().AddDebug())
                .ConfigureServices(services =>
                {
                    services.AddSingleton<IHostedService, BattlesReactorHostedService>()
                        .AddMediatR(typeof(Program))
                        .AddJsonAppSettings()
                        .AddMongoSyncStorage()
                        .AddDefaultJsonSettings()
                        .AddPomeloEventStore()
                        .AddPersistentSubscriptions();
                });

            return builder.UseConsoleLifetime()
                .Build()
                .UsePomeloEventStoreEventsLogging()
                .RunAsync();
        }
    }
}
