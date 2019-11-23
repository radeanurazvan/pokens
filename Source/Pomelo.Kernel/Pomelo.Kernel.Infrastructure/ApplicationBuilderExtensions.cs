using Microsoft.AspNetCore.Builder;

namespace Pomelo.Kernel.Infrastructure
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UsePomeloCors(this IApplicationBuilder app)
        {
            return app.UseCors("PomeloCors");
        }

        public static IApplicationBuilder UsePomeloSwagger(this IApplicationBuilder app, string title)
        {
            return app.UseSwagger()
                .UseSwaggerUI(c => { c.SwaggerEndpoint("../swagger/v1/swagger.json", title); });
        }
    }
}