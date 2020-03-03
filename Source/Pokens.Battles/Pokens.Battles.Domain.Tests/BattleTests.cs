using System;
using System.Linq;
using CSharpFunctionalExtensions;
using FluentAssertions;
using Pokens.Battles.Resources;
using Pomelo.Kernel.Domain;
using Xunit;

namespace Pokens.Battles.Domain.Tests
{
    public sealed class BattleTests
    {
        [Fact]
        public void Given_CreateProcess_Should_CreateCorrectBattle()
        {
            // Arrange
            var challengeId = new Guid("AE5FC90F-D302-422D-B48C-45E99029F9FD");
            var arena = ArenaFactory.WithoutRequirement();
            var attacker = TrainerFactory.WithLevel(1);
            var defender = TrainerFactory.WithLevel(1);
            var now = DateTime.UtcNow;
            TimeProviderContext.AdvanceTimeTo(now);

            // Act
            var battleResult = Battle.FromChallenge(challengeId)
                .Bind(b => b.In(arena.Id))
                .Bind(b => b.WithAttacker(attacker.Id, attacker.Pokemons.First()))
                .Bind(b => b.WithDefender(defender.Id, defender.Pokemons.First()));

            // Assert
            battleResult.IsSuccess.Should().BeTrue();
            battleResult.Value.Id.Should().Be(challengeId);
            battleResult.Value.ArenaId.Should().Be(arena.Id);
            battleResult.Value.ActivePlayer.Should().Be(attacker.Id);
            battleResult.Value.AttackerId.Should().Be(attacker.Id);
            battleResult.Value.AttackerPokemon.Offensive.Should().BeEquivalentTo(attacker.Pokemons.First().Stats.Offensive);
            battleResult.Value.DefenderId.Should().Be(defender.Id);
            battleResult.Value.DefenderPokemon.Offensive.Should().BeEquivalentTo(defender.Pokemons.First().Stats.Offensive);
            battleResult.Value.StartedAt.Should().Be(now);
            battleResult.Value.EndedAt.Should().Be(Maybe<DateTime>.None);
        }

        [Fact]
        public void Given_TakeTurn_When_TrainerIsInvalid_Then_ShouldFail()
        {
            // Arrange
            var sut = BattleFactory.Started();
            var randomTrainer = TrainerFactory.WithLevel(1);

            // Act
            var result = sut.TakeTurn(null, randomTrainer.Pokemons.First().Abilities.First());

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(Messages.InvalidTrainer);
        }

        [Fact]
        public void Given_TakeTurn_When_AbilityIsInvalid_Then_ShouldFail()
        {
            // Arrange
            var sut = BattleFactory.Started();
            var randomTrainer = TrainerFactory.WithLevel(1);

            // Act
            var result = sut.TakeTurn(randomTrainer, null);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(Messages.InvalidAbility);
        }

        [Fact]
        public void Given_TakeTurn_When_TrainerIsNotParticipant_Then_ShouldFail()
        {
            // Arrange
            var arena = ArenaFactory.WithoutRequirement();
            var attacker = TrainerFactory.EnrolledIn(arena);
            var defender = TrainerFactory.EnrolledIn(arena);
            var sut = BattleFactory.Started(attacker, defender);
            var randomTrainer = TrainerFactory.WithLevel(1);

            // Act
            var result = sut.TakeTurn(randomTrainer, randomTrainer.Pokemons.First().Abilities.First());

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(Messages.YouAreNotTheActivePlayer);
        }

        [Fact]
        public void Given_TakeTurn_When_TrainerIsNoActivPlayer_Then_ShouldFail()
        {
            // Arrange
            var arena = ArenaFactory.WithoutRequirement();
            var attacker = TrainerFactory.EnrolledIn(arena);
            var defender = TrainerFactory.EnrolledIn(arena);
            var sut = BattleFactory.Started(attacker, defender);

            // Act
            var result = sut.TakeTurn(defender, defender.Pokemons.First().Abilities.First());

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(Messages.YouAreNotTheActivePlayer);
        }

        [Fact]
        public void Given_TakeTurn_When_BattleAlreadyEnded_Then_ShouldFail()
        {
            // Arrange
            var arena = ArenaFactory.WithoutRequirement();
            var attacker = TrainerFactory.EnrolledIn(arena);
            var defender = TrainerFactory.EnrolledIn(arena);
            var sut = BattleFactory.Ended(attacker, defender);

            // Act
            var result = sut.TakeTurn(attacker, attacker.Pokemons.First().Abilities.First());

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(Messages.BattleAlreadyEnded);
        }

        [Fact]
        public void Given_TakeTurn_When_AbilityIsOnCooldown_Then_ShouldFail()
        {
            // Arrange
            var arena = ArenaFactory.WithoutRequirement();
            var attacker = TrainerFactory.EnrolledIn(arena);
            var ability = AbilityFactory.WithCooldown(100);
            var pokemon = PokemonFactory.Pikachu(attacker.Id, ability);
            attacker.Catch(pokemon);

            var defender = TrainerFactory.EnrolledIn(arena);
            var sut = BattleFactory.Started(attacker, defender);
            sut.TakeTurn(attacker, ability);
            sut.TakeTurn(defender, defender.Pokemons.First().Abilities.First());

            // Act
            var result = sut.TakeTurn(attacker, ability);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(Messages.AbilityIsOnCooldown);
        }

        [Fact]
        public void Given_TakeTurn_When_AbilityIsNotOnCooldown_Then_ShouldDealDamage()
        {
            // Arrange
            var arena = ArenaFactory.WithoutRequirement();
            var attacker = TrainerFactory.EnrolledIn(arena);
            var pokemon = PokemonFactory.Pikachu(attacker.Id, AbilityFactory.WithCooldown(0), AbilityFactory.WithCooldown(1));
            attacker.Catch(pokemon);

            var defender = TrainerFactory.EnrolledIn(arena);
            var sut = BattleFactory.Started(attacker, defender);
            sut.TakeTurn(attacker, pokemon.Abilities.Last());
            sut.TakeTurn(defender, defender.Pokemons.First().Abilities.First());

            // Act
            var initialHealth = defender.Pokemons.First().Stats.Defensive.Health;

            var failingResult = sut.TakeTurn(attacker, pokemon.Abilities.Last());
            sut.TakeTurn(attacker, pokemon.Abilities.First());
            sut.TakeTurn(defender, defender.Pokemons.First().Abilities.First());

            var result = sut.TakeTurn(attacker, pokemon.Abilities.Last());

            // Assert
            failingResult.IsFailure.Should().BeTrue();
            failingResult.Error.Should().Be(Messages.AbilityIsOnCooldown);
            result.IsSuccess.Should().BeTrue();
            sut.DefenderPokemon.Defensive.Health.Should().BeLessThan(initialHealth);
        }

        [Fact]
        public void Given_TakeTurn_Should_SwitchPlayers()
        {
            // Arrange
            var arena = ArenaFactory.WithoutRequirement();
            var attacker = TrainerFactory.EnrolledIn(arena);
            var defender = TrainerFactory.EnrolledIn(arena);

            var attackerPokemon = attacker.Pokemons.First();
            var defenderPokemon = defender.Pokemons.First();

            var sut = BattleFactory.Started(attacker, defender);

            // Act
            sut.TakeTurn(attacker, attackerPokemon.Abilities.First());
            sut.TakeTurn(defender, defenderPokemon.Abilities.First());
            sut.TakeTurn(attacker, attackerPokemon.Abilities.First());

            // Assert
            sut.ActivePlayer.Should().Be(sut.DefenderId);

            var playerChangedEvents = sut.Events.Where(e => e is ActivePlayerChangedEvent).Cast<ActivePlayerChangedEvent>().ToList();
            playerChangedEvents.ElementAt(0).ActivePlayer.Should().Be(sut.DefenderId);
            playerChangedEvents.ElementAt(1).ActivePlayer.Should().Be(sut.AttackerId);
            playerChangedEvents.ElementAt(2).ActivePlayer.Should().Be(sut.DefenderId);
        }
    }
}