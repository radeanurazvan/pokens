using System;
using System.Linq;
using CSharpFunctionalExtensions;
using FluentAssertions;
using Pokens.Battles.Domain.Tests.Extensions;
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

            // Act
            var battleResult = Battle.FromChallenge(challengeId)
                .Bind(b => b.In(arena.Id))
                .Bind(b => b.WithAttacker(attacker.Id, attacker.Pokemons.First()))
                .Bind(b => b.WithDefender(defender.Id, defender.Pokemons.First()));

            // Assert
            battleResult.IsSuccess.Should().BeTrue();
            battleResult.Value.Id.Should().Be(challengeId);
            battleResult.Value.ArenaId.Should().Be(arena.Id);
            battleResult.Value.AttackerId.Should().Be(attacker.Id);
            battleResult.Value.AttackerPokemon.Id.Should().Be(attacker.FirstPokemonId());
            battleResult.Value.DefenderId.Should().Be(defender.Id);
            battleResult.Value.DefenderPokemon.Id.Should().Be(defender.FirstPokemonId());
        }
    }
}