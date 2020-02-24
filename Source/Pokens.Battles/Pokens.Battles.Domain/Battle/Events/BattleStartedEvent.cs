using System;
using Pomelo.Kernel.Domain;
using Pomelo.Kernel.Events.Abstractions;

namespace Pokens.Battles.Domain.Events
{
    internal sealed class BattleStartedEvent : IDomainEvent
    {
        private BattleStartedEvent()
        {
        }

        public BattleStartedEvent(Battle battle)
            : this()
        {
            Id = battle.Id;
            ArenaId = battle.ArenaId;
            AttackerId = battle.AttackerId;
            AttackerPokemon = battle.AttackerPokemon;
            DefenderId = battle.DefenderId;
            DefenderPokemon = battle.DefenderPokemon;
            StartedAt = TimeProvider.Instance().UtcNow;
        }

        public Guid Id { get; private set; }

        public Guid ArenaId { get; private set; }

        public Guid AttackerId { get; private set; }

        public Pokemon AttackerPokemon { get; private set; }

        public Pokemon DefenderPokemon { get; private set; }

        public Guid DefenderId { get; private set; }

        public DateTime StartedAt { get; private set; }
    }
}