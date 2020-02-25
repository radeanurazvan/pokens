using System;
using Pomelo.Kernel.Domain;
using Pomelo.Kernel.Events.Abstractions;

namespace Pokens.Battles.Domain
{
    internal sealed class BattleEndedEvent : IDomainEvent
    {
        private BattleEndedEvent()
        {
        }

        public BattleEndedEvent(Battle battle)
            : this()
        {
            BattleId = battle.Id;
            Winner = battle.ActivePlayer;
            Loser = battle.WaitingPlayer;
            EndedAt = TimeProvider.Instance().UtcNow;
        }

        public Guid BattleId { get; private set; }

        public Guid Winner { get; private set; }

        public Guid Loser { get; private set; }

        public DateTime EndedAt { get; private set; }
    }
}