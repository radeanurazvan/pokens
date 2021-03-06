using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Pokens.Battles.Business;
using Pokens.Battles.Domain;
using Pokens.Battles.Infrastructure;
using Pomelo.Kernel.EventStore;
using Pomelo.Kernel.Http;

namespace Pokens.Battles.Write.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddBattlesDomain()
                .AddBattlesBusiness()
                .AddBattlesInfrastructure()
                .AddBattlesAuthorization(Configuration)
                .AddPomeloSwagger("Pokens Battles API")
                .AddPomeloCors(Configuration)
                .AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app
                .UseAuthentication()
                .UseHttpsRedirection()
                .UseRouting()
                .UseAuthorization()
                .UsePomeloEventStoreConnection()
                .UseBattlesSubscriptions()
                .UsePomeloSwagger("Pokens Battle Write API")
                .UsePomeloCors()
                .UseDefaultArenas()
                .UsePomeloEventStoreEventsLogging()
                .UseBattlesEndpoints();
        }
    }
}
