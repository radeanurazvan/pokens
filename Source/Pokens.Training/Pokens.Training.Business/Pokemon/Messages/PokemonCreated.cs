using System;
using Pomelo.Kernel.Messaging.Abstractions;

namespace Pokens.Training.Business
{
    internal sealed class PokemonCreated : IBusMessage
    {
        private PokemonCreated()
        {
        }

        public Guid Id { get; private set; }

        public string Name { get; private set; }

        public int Health { get; private set; }

        public int Defense { get; private set; }

        public float DodgeChance { get; private set; }

        public int AttackPower { get; private set; }

        public float CriticalStrikeChance { get; private set; }

        public double CatchRate { get; private set; }
    }
}