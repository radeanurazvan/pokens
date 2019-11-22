using System;
using Microsoft.AspNetCore.Identity;

namespace Pokens.Trainers.Domain
{
    public sealed class Credentials : IdentityUser<Guid>
    {
        public Credentials(string email, Guid trainerId)
        {
            Email = email;
            UserName = email;
            TrainerId = trainerId;
        }

        public Guid TrainerId { get; private set; }
    }
}