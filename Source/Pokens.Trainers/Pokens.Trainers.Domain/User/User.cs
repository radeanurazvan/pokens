using System;
using Microsoft.AspNetCore.Identity;

namespace Pokens.Trainers.Domain
{
    public sealed class User : IdentityUser<Guid>
    {
        public User(string email)
        {
            Id = Guid.NewGuid();
            Email = email;
            UserName = email;
        }
    }
}