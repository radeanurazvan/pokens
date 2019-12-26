using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Pokens.Training.Business;
using Pokens.Training.Infrastructure;
using Pomelo.Kernel.Infrastructure;

namespace Pokens.Training.Api
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
                .AddHttpContextAccessor()
                .AddDefaultJsonSettings()
                .AddLogging(c => c.AddDebug().AddConsole())
                .AddTrainingBusiness()
                .AddTrainingInfrastructure()
                .AddPomeloJwtAuthentication(Configuration.GetJwtSettings())
                .AddPomeloSwagger("Pokens Training Api")
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
                .UsePomeloSwagger("Pokens Training")
                .UseHttpsRedirection()
                .UseRouting()
                .UseAuthorization()
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                })
                .UseTrainingBusSubscriptions();
        }
    }
}
