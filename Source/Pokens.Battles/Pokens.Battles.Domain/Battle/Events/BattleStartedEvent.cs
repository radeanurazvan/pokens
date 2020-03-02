using System;
using Pomelo.Kernel.Domain;
using Pomelo.Kernel.Events.Abstractions;

namespace Pokens.Battles.Domain
{
    public sealed class BattleStartedEvent : IDomainEvent
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
            AttackerPokemon = new BattlePokemonStats(battle.AttackerPokemon);
            DefenderId = battle.DefenderId;
            DefenderPokemon = new BattlePokemonStats(battle.DefenderPokemon);
            StartedAt = TimeProvider.Instance().UtcNow;
        }

        public Guid Id { get; private set; }

        public Guid ArenaId { get; private set; }

        public Guid AttackerId { get; private set; }

        public BattlePokemonStats AttackerPokemon { get; private set; }

        public BattlePokemonStats DefenderPokemon { get; private set; }

        public Guid DefenderId { get; private set; }

        public DateTime StartedAt { get; private set; }

        public sealed class BattlePokemonStats
        {
            private BattlePokemonStats()
            {
            }

            internal BattlePokemonStats(PokemonInFight pokemon)
                : this()
            {
                Id = pokemon.Id;
                Health = pokemon.Defensive.Health;
                Defense = pokemon.Defensive.Defense;
                DodgeChange = pokemon.Defensive.DodgeChance;
                AttackPower = pokemon.Offensive.AttackPower;
                CriticalStrikeChance = pokemon.Offensive.CriticalStrikeChance;
            }

            public Guid Id { get; private set; }

            public int Health { get; private set; }

            public int Defense { get; private set; }

            public float DodgeChange { get; private set; }

            public int AttackPower { get; private set; }

            public float CriticalStrikeChance { get; private set; }
        }
    }
}