﻿using System;
using Pomelo.Kernel.Domain;

namespace Pokens.Training.Business
{
    internal sealed class TrainerCreatedEvent : IDomainEvent
    {
        private TrainerCreatedEvent()
        {
        }

        public Guid Id { get; private set; }

        public string Name { get; private set; }
    }
}