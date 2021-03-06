using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Pokens.Pokedex.Business;
using Pokens.Pokedex.Infrastructure;
using Pomelo.Kernel.Authentication;
using Pomelo.Kernel.Http;

namespace Pokens.Pokedex.Api
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
                .AddPokedexInfrastructure("Pokedex.Api")
                .AddPokedexServices()
                .AddPomeloJwtAuthentication(Configuration.GetJwtSettings())
                .AddPomeloSwagger("Pokens Pokemons Api")
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
                .UsePomeloCors()
                .UsePomeloSwagger("Pokens Pokemons v1")
                .UseHttpsRedirection()
                .UseRouting()
                .UseAuthorization()
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });
        }
    }
}
