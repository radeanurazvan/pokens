using Microsoft.Extensions.Configuration;

namespace Pomelo.Kernel.Infrastructure
{
    public static class ConfigurationExtensions
    {
        public static IJwtSettings GetJwtSettings(this IConfiguration configuration, string sectionKey = nameof(JwtSettings)) 
            => configuration.GetSection(sectionKey).Get<JwtSettings>(o => o.BindNonPublicProperties = true);
    }
}