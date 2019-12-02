using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Pokemons.Trainers.Business;
using Pokens.Trainers.Infrastructure;
using Pokens.Trainers.Persistence.EntityFramework;
using Pomelo.Kernel.Infrastructure;
using Pomelo.Kernel.Messaging;

namespace Pokens.Trainers.Api
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
                .AddTrainersContext(Configuration)
                .AddTrainersInfrastructure()
                .AddTrainersIdentity()
                .AddTrainersJwtAuthentication(Configuration)
                .AddPomeloSwagger("Pokens Trainers Api")
                .AddPomeloCors(Configuration)
                .AddMediatR(typeof(BusinessLayer))
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
                .UsePomeloCors()
                .UsePomeloSwagger("Pokens Trainers v1")
                .UseHttpsRedirection()
                .UseRouting()
                .UseAuthorization()
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                })
                .UseTrainersMigrations();
        }
    }
}
