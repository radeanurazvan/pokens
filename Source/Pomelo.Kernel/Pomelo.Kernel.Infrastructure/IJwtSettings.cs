using Microsoft.IdentityModel.Tokens;

namespace Pomelo.Kernel.Infrastructure
{
    public interface IJwtSettings
    {
        string Issuer { get; }

        string Audience { get; }

        SecurityKey SecurityKey { get; }
    }
}