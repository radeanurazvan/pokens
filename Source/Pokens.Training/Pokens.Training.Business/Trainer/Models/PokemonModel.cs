using System.Collections.Generic;
using System.Linq;
using Pokens.Training.Domain;

namespace Pokens.Training.Business
{
    public sealed class PokemonModel
    {
        public PokemonModel(string trainerId, Pokemon pokemon)
        {
            Id = pokemon.Id;
            DefinitionId = pokemon.DefinitionId;
            TrainerId = trainerId;
            Name = pokemon.Name;
            Image = pokemon.Image;
            Level = pokemon.Level;
            Abilities = pokemon.Abilities.Select(a => new AbilityModel(a));
        }

        public string Id { get; }

        public string DefinitionId { get; }

        public string TrainerId { get; }

        public string Name { get; }

        public int Level { get; private set; }

        public IEnumerable<AbilityModel> Abilities { get; private set; }

        public byte[] Image { get; set; }
    }

    public sealed class AbilityModel
    {
        public AbilityModel(Ability ability)
        {
            Id = ability.Id;
            Name = ability.Name;
            Description = ability.Description;
            RequiredLevel = ability.RequiredLevel;
            Cooldown = ability.Cooldown;
            Image = ability.Image;
        }

        public string Id { get; private set; }

        public string Name { get; private set; }

        public string Description { get; private set; }

        public int RequiredLevel { get; private set; }

        public int Cooldown { get; private set; }

        public byte[] Image { get; private set; }
    }
}