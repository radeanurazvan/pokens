using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Pokens.Trainers.Persistence.EntityFramework
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseTrainersMigrations(this IApplicationBuilder app)
        {
            using var scope  = app.ApplicationServices.CreateScope();
            
            var context = scope.ServiceProvider.GetService<DbContext>();
            context.Database.Migrate();
            return app;
        }
    }
}