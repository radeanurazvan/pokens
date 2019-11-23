using System;
using System.Collections.Generic;
using CSharpFunctionalExtensions;

namespace Pokens.Trainers.Domain
{
    public sealed class AuthenticationToken : ValueObject
    {
        public AuthenticationToken(string token, DateTime expiresAt)
        {
            Token = token;
            ExpiresAt = expiresAt;
        }

        public string Token { get; private set; }

        public DateTime ExpiresAt { get; private set; }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Token;
            yield return ExpiresAt;
        }
    }
}