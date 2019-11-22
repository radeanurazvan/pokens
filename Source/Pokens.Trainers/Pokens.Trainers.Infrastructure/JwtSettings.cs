using System;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Pokens.Trainers.Infrastructure
{
    public class JwtSettings
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