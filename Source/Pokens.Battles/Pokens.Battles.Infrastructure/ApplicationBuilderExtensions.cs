using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Pokens.Battles.Domain;
using Pomelo.Kernel.Common;
using Pomelo.Kernel.Domain;

namespace Pokens.Battles.Infrastructure
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseDefaultArenas(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var repository = scope.ServiceProvider.GetService<IWriteRepository<Arena>>();
                Constants.DefaultArenas.ForEach(a => repository.Add(a).GetAwaiter().GetResult());
                repository.Save().GetAwaiter().GetResult();
            }

            return app;
        }
    }
}