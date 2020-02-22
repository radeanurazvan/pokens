using Pomelo.Kernel.Domain;
using Pomelo.Kernel.Events.Abstractions;

namespace Pokens.Battles.Domain
{
    internal sealed class TrainerCaughtPokemonEvent : IDomainEvent
    {
        private TrainerCaughtPokemonEvent()
        {
        }

        public TrainerCaughtPokemonEvent(Pokemon pokemon)
            : this()
        {
            Pokemon = pokemon;
        }

        public Pokemon Pokemon { get; private set; }
    }
}