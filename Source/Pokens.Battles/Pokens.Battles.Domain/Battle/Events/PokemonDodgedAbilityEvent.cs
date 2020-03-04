using System;
using Pomelo.Kernel.Events.Abstractions;

namespace Pokens.Battles.Domain
{
    public sealed class PokemonDodgedAbilityEvent : IDomainEvent
    {
        private PokemonDodgedAbilityEvent()
        {
        }

        public PokemonDodgedAbilityEvent(Guid battleId, Guid trainerId)
            : this()
        {
            BattleId = battleId;
            TrainerId = trainerId;
        }

        public Guid TrainerId { get; private set; }

        public Guid BattleId { get; private set; }
    }
}