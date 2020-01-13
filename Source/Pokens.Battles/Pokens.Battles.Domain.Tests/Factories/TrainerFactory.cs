using System;
using System.Linq;
using Pokens.Battles.Domain.Tests.Extensions;

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

        public static Trainer ChallengedBy(Trainer challenger)
        {
            var challenged = WithLevel(1);
            challenger.Challenge(challenged, challenger.FirstPokemonId(), challenged.FirstPokemonId());
            challenger.ClearEvents();
            challenged.ClearEvents();

            return challenged;
        }

        public static Trainer ChallengedWith(Trainer challenger, Arena arena)
        {
            var challenged = WithLevel(1);
            challenged.EnrollIn(arena);
            challenger.Challenge(challenged, challenger.FirstPokemonId(), challenged.FirstPokemonId());
            
            challenger.ClearEvents();
            challenged.ClearEvents();

            return challenged;
        }

        public static Trainer WithChallengeAcceptedFrom(Trainer challenger)
        {
            var challenged = WithLevel(1);
            challenger.Challenge(challenged, challenger.FirstPokemonId(), challenged.FirstPokemonId());
            challenged.AcceptChallenge(challenger, challenged.Challenges.First());
            
            challenger.ClearEvents();
            challenged.ClearEvents();

            return challenged;
        }

        public static Trainer InBattle(Trainer challenger)
        {
            var challenged = WithLevel(1);
            challenger.Challenge(challenged, challenger.FirstPokemonId(), challenged.FirstPokemonId());
            challenged.AcceptChallenge(challenger, challenged.Challenges.First());

            challenger.ClearEvents();
            challenged.ClearEvents();

            return challenged;
        }

        public static Trainer Enrolled() => EnrolledIn(ArenaFactory.WithoutRequirement());
    }
}