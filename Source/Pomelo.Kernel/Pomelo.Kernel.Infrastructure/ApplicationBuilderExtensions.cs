using Microsoft.AspNetCore.Builder;

namespace Pomelo.Kernel.Infrastructure
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UsePomeloCors(this IApplicationBuilder app)
        {
            return app.UseCors("PomeloCors");
        }
    }
}