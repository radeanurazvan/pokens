using System;
using Pomelo.Kernel.Domain;

namespace Pokens.Battles.Domain
{
    public sealed class TrainerInArena : Entity
    {
        public TrainerInArena(Guid id, string name, DateTime joinedAt)
        {
            Id = id;
            Name = name;
            JoinedAt = joinedAt;
        }

        public string Name { get; private set; }

        public DateTime JoinedAt { get; private set; }
    }
}