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

        public BattleEndedEvent(Guid winner, Guid loser)
            : this()
        {
            Winner = winner;
            Loser = loser;
            EndedAt = TimeProvider.Instance().UtcNow;
        }

        public Guid Winner { get; private set; }

        public Guid Loser { get; private set; }

        public DateTime EndedAt { get; private set; }
    }
}