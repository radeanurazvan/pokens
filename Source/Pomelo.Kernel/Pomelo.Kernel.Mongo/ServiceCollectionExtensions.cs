using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Pomelo.Kernel.Common;
using Pomelo.Kernel.DataSynchronization;
using Pomelo.Kernel.Domain;

namespace Pomelo.Kernel.Mongo
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMongoSyncStorage(this IServiceCollection services)
        {
            return services.AddSingletonSettings<MongoSettings>()
                .AddScoped(ctx =>
                {
                    var settings = ctx.GetService<MongoSettings>();
                    return new MongoClient(settings.ConnectionString);
                })
                .AddScoped<ISyncStorage, MongoSyncStorage>()
                .AddScoped(typeof(ISyncReadRepository<>), typeof(MongoSyncReadRepository<>));
        }

        public static IServiceCollection AddPomeloMongoCollectionRepository(this IServiceCollection services)
        {
            return services.AddSingletonSettings<MongoSettings>()
                .AddScoped<ICollectionRepository, MongoCollectionRepository>();
        }
    }
}