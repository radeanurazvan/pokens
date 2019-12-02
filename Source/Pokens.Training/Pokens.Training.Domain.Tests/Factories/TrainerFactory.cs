using System;

namespace Pokens.Training.Domain.Tests
{
    public static class TrainerFactory
    {
        public static Trainer Ash() => Trainer.Create(Guid.NewGuid(), "Ash").Value;

        public static Trainer AshWithPikachu()
        {
            var ash = Ash();
            ash.ChooseStarter(PokemonDefinitionFactory.Starter("Pikachu"));

            return ash;
        }

    }
}