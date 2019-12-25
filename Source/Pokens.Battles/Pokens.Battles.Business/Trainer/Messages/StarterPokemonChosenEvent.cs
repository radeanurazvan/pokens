using Pomelo.Kernel.Domain;

namespace Pokens.Battles.Business
{
    internal sealed class StarterPokemonChosenEvent : IDomainEvent
    {
        private StarterPokemonChosenEvent()
        {
        }

        public string PokemonId { get; private set; }

        public string DefinitionName { get; private set; }

        public int Health { get; private set; }

        public int Defense { get; private set; }

        public float DodgeChance { get; private set; }

        public int AttackPower { get; private set; }

        public float CriticalStrikeChance { get; private set; }
    }
}