using System;
using Pomelo.Kernel.Domain;
using Pomelo.Kernel.Events.Abstractions;

namespace Pokens.Battles.Domain
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
            AttackerPokemon = new BattlePokemonStats(battle.AttackerPokemon.Offensive, battle.AttackerPokemon.Defensive);
            DefenderId = battle.DefenderId;
            DefenderPokemon = new BattlePokemonStats(battle.DefenderPokemon.Offensive, battle.DefenderPokemon.Defensive);
            StartedAt = TimeProvider.Instance().UtcNow;
        }

        public Guid Id { get; private set; }

        public Guid ArenaId { get; private set; }

        public Guid AttackerId { get; private set; }

        public BattlePokemonStats AttackerPokemon { get; private set; }

        public BattlePokemonStats DefenderPokemon { get; private set; }

        public Guid DefenderId { get; private set; }

        public DateTime StartedAt { get; private set; }


        internal sealed class BattlePokemonStats
        {
            private BattlePokemonStats()
            {
            }

            public BattlePokemonStats(OffensiveStats offensive, DefensiveStats defensive)
                : this()
            {
                Health = defensive.Health;
                Defense = defensive.Defense;
                DodgeChange = defensive.DodgeChance;
                AttackPower = offensive.AttackPower;
                CriticalStrikeChance = offensive.CriticalStrikeChance;
            }

            public int Health { get; private set; }

            public int Defense { get; private set; }

            public float DodgeChange { get; private set; }

            public int AttackPower { get; private set; }

            public float CriticalStrikeChance { get; private set; }
        }
    }
}