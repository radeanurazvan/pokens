using System;
using Pomelo.Kernel.Events.Abstractions;

namespace Pokens.Battles.Domain
{
    public sealed class ActivePlayerChangedEvent : IDomainEvent
    {
        private ActivePlayerChangedEvent()
        {
        }

        public ActivePlayerChangedEvent(Battle battle)
             : this()
        {
            BattleId = battle.Id;
            ActivePlayer = battle.ActivePlayer;
            ActivePokemon = battle.ActivePokemon.Id;
        }

        public Guid BattleId { get; private set; }

        public Guid ActivePlayer { get; private set; }

        public Guid ActivePokemon { get; private set; }
    }
}