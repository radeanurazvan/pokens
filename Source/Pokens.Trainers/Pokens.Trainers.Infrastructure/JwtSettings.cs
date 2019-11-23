using System;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Pomelo.Kernel.Infrastructure;

namespace Pokens.Trainers.Infrastructure
{
    public class JwtSettings : IJwtSettings
    {
        public string Key { get; private set; }

        public string Issuer { get; private set; }

        public string Audience { get; private set; }

        public int TokenValidityInMinutes { get; private set; }

        public TimeSpan TokenValidity => TimeSpan.FromMinutes(this.TokenValidityInMinutes);

        public SecurityKey SecurityKey => new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.Key));

        public string SecurityAlgorithm => SecurityAlgorithms.HmacSha256;
    }
}