using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pokens.Trainers.Domain;

namespace Pokens.Trainers.Persistence.EntityFramework
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddTrainersContext(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddScoped(p =>
            {
                var configuration = p.GetService<IConfiguration>();
                var options = new DbContextOptionsBuilder()
                    .UseSqlServer(configuration.GetConnectionString("Trainers"))
                    .Options;
                return new TrainersContext(options);
            })
                .AddScoped<DbContext>(p =>
                {
                    var configuration = p.GetService<IConfiguration>();
                    var options = new DbContextOptionsBuilder()
                        .UseSqlServer(configuration.GetConnectionString("Trainers"))
                        .Options;
                    return new TrainersContext(options);
                })
                .AddDbContext<TrainersContext>(o => o.UseSqlServer(configuration.GetConnectionString("Trainers")));
        }

        public static IServiceCollection AddTrainersIdentity(this IServiceCollection services)
        {
            services.AddIdentity<User, IdentityRole<Guid>>(cfg => { cfg.User.RequireUniqueEmail = true; })
                .AddEntityFrameworkStores<TrainersContext>();
            return services;
        }
    }
}