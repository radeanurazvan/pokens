using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using CSharpFunctionalExtensions;
using Microsoft.IdentityModel.Tokens;
using Pokens.Trainers.Domain;
using Pomelo.Kernel.Common;
using Pomelo.Kernel.Infrastructure;

namespace Pokens.Trainers.Infrastructure
{
    internal sealed class JwtTokenService : ITokenService
    {
        private readonly JwtSettings settings;

        public JwtTokenService(JwtSettings settings)
        {
            this.settings = settings;
        }

        public Result<AuthenticationToken> Exchange(User user)
        {
            return user.EnsureExists("Invalid credentials!")
                .Map(CreateToken);
        }

        private AuthenticationToken CreateToken(User user)
        {
            var now = DateTime.Now;
            var expirationTime = now.Add(settings.TokenValidity);

            var signingCredentials = new SigningCredentials(settings.SecurityKey, settings.SecurityAlgorithm);

            var jwtToken = new JwtSecurityToken(
                settings.Issuer,
                settings.Audience,
                GetClaims(user),
                now,
                expirationTime,
                signingCredentials);

            var encodedToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            return new AuthenticationToken(encodedToken, expirationTime);
        }

        private IEnumerable<Claim> GetClaims(User user)
        {
            return new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Iss, settings.Issuer),
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(PokensClaims.TrainerId, user.Trainer.Id.ToString()),
                new Claim(PokensClaims.TrainerName, user.Trainer.Name)
            };
        }
    }
}