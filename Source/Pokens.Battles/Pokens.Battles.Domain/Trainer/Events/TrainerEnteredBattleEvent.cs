﻿using System;
using Pomelo.Kernel.Domain;

namespace Pokens.Battles.Domain
{
    internal sealed class TrainerEnteredBattleEvent : IDomainEvent
    {
        private TrainerEnteredBattleEvent()
        {
        }

        public TrainerEnteredBattleEvent(Guid enemy)
            : this()
        {
            Enemy = enemy;
        }

        public Guid Enemy { get; private set; }
    }
}