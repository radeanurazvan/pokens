using MongoDB.Bson.Serialization;
using Pokens.Training.Domain;

namespace Pokens.Training.Infrastructure
{
    internal class TrainerMap : BsonClassMap<Trainer>
    {
        public TrainerMap()
        {
            AutoMap();

            UnmapProperty(t => t.StarterPokemon);
            UnmapProperty(t => t.CaughtPokemons);
            UnmapProperty(t => t.Events);

            MapField(Trainer.Expressions.StarterPokemon).SetElementName(nameof(Trainer.StarterPokemon));
            MapField(Trainer.Expressions.CaughtPokemons).SetElementName(nameof(Trainer.CaughtPokemons));
        }
    }
}