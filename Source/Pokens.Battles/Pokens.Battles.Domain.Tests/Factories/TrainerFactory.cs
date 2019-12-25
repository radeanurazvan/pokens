using System;

namespace Pokens.Battles.Domain.Tests
{
    public static class TrainerFactory
    {
        public static Trainer WithLevel(int level)
        {
            var trainer = Trainer.Register(Guid.NewGuid(), "Ash");
            trainer.Catch(PokemonFactory.Pikachu(level));
            trainer.ClearEvents();
            return trainer;
        }

        public static Trainer EnrolledIn(Arena arena)
        {
            var trainer = WithLevel(arena.RequiredLevel + 1);
            arena.Enroll(trainer);
            trainer.ClearEvents();
            arena.ClearEvents();
            return trainer;
        }

        public static Trainer Enrolled() => EnrolledIn(ArenaFactory.WithoutRequirement());
    }
}