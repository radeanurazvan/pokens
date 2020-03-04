using System.Linq;
using CSharpFunctionalExtensions;
using Pokens.Battles.Domain.Tests.Extensions;

namespace Pokens.Battles.Domain.Tests
{ 
    internal static class BattleFactory
    {
        public static Battle Started()
        {
            var arena = ArenaFactory.WithoutRequirement();
            var attacker = TrainerFactory.EnrolledIn(arena);
            var defender = TrainerFactory.EnrolledIn(arena);

            return Started(attacker, defender);
        }

        public static Battle Started(Trainer attacker, Trainer defender) => Started(attacker, defender, attacker.Pokemons.First(), defender.Pokemons.First());

        public static Battle Started(Trainer attacker, Trainer defender, Pokemon attackerPokemon, Pokemon defenderPokemon)
        {
            attacker.Challenge(defender, attackerPokemon.Id, defenderPokemon.Id);
            var challenge = attacker.Challenges.First();

            defender.AcceptChallenge(attacker, challenge);
            attacker.StartBattleAgainst(defender, challenge.Id);

            attacker.ClearEvents();
            defender.ClearEvents();
            var battle = Battle.FromChallenge(challenge.Id)
                .Bind(b => b.In(challenge.ArenaId))
                .Bind(b => b.WithAttacker(attacker.Id, attackerPokemon))
                .Bind(b => b.WithDefender(defender.Id, defenderPokemon))
                .Value;
            battle.ClearEvents();

            return battle;
        }

        public static Battle Ended(Trainer attacker, Trainer defender)
        {
            var activePlayer = attacker;

            var battle = Started(attacker, defender);
            while (battle.IsOnGoing)
            {
                battle.TakeTurn(activePlayer, activePlayer.Pokemons.First().Abilities.First());
                activePlayer = activePlayer == attacker ? defender : attacker;
            }

            battle.ClearEvents();
            return battle;
        }
    }
}