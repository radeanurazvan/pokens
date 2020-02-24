using System;
using CSharpFunctionalExtensions;
using Pokens.Battles.Domain.Events;
using Pokens.Battles.Resources;
using Pomelo.Kernel.Common;
using Pomelo.Kernel.Domain;

namespace Pokens.Battles.Domain
{
    public sealed class Battle : AggregateRoot, IIdentifiedBattle, IBattleWithArena, IBattleWithAttacker
    {
        private Battle()
        {
        }

        public static Result<IIdentifiedBattle> FromChallenge(Guid challengeId)
        {
            return Result.FailureIf(challengeId == Guid.Empty, Messages.ChallengeNotFound)
                .Map(() => new Battle { Id = challengeId } as IIdentifiedBattle);
        }

        public Result<IBattleWithArena> In(Guid arenaId)
        { 
            return Result.FailureIf(arenaId == Guid.Empty, Messages.ArenaNotFound)
                .Tap(() => ArenaId = arenaId)
                .Map(() => this as IBattleWithArena);
        }

        public Result<IBattleWithAttacker> WithAttacker(Guid attackerId, Pokemon attackerPokemon)
        {
            var trainerResult = Result.FailureIf(attackerId == Guid.Empty, Messages.InvalidTrainer);
            var pokemonResult = attackerPokemon.EnsureExists(Messages.InvalidPokemon)
                .Ensure(p => p.TrainerId == attackerId, Messages.TrainerDoesNotOwnPokemon);

            return Result.FirstFailureOrSuccess(trainerResult, pokemonResult)
                .Tap(() => AttackerId = attackerId)
                .Tap(() => AttackerPokemon = attackerPokemon)
                .Map(() => this as IBattleWithAttacker);
        }

        public Result<Battle> WithDefender(Guid defenderId, Pokemon defenderPokemon)
        {
            var trainerResult = Result.FailureIf(defenderId == Guid.Empty, Messages.InvalidTrainer);
            var pokemonResult = defenderPokemon.EnsureExists(Messages.InvalidPokemon)
                .Ensure(p => p.TrainerId == defenderId, Messages.TrainerDoesNotOwnPokemon);

            return Result.FirstFailureOrSuccess(trainerResult, pokemonResult)
                .Tap(() => DefenderId = defenderId)
                .Tap(() => DefenderPokemon = defenderPokemon)
                .Tap(() => ReactToDomainEvent(new BattleStartedEvent(this)))
                .Map(() => this);
        }

        public Guid ArenaId { get; private set; }

        public Guid AttackerId { get; private set; }

        public Pokemon AttackerPokemon { get; private set; }
        
        public Guid DefenderId { get; private set; }

        public Pokemon DefenderPokemon { get; private set; }

        public DateTime StartedAt { get; private set; }

        public Maybe<DateTime> EndedAt { get; private set; } = Maybe<DateTime>.None;

        private void When(BattleStartedEvent @event)
        {
            Id = @event.Id;
            ArenaId = @event.ArenaId;
            AttackerId = @event.AttackerId;
            AttackerPokemon = @event.AttackerPokemon;
            DefenderId = @event.DefenderId;
            DefenderPokemon = @event.DefenderPokemon;
            StartedAt = @event.StartedAt;
        }
    }

    public interface IIdentifiedBattle
    {
        Result<IBattleWithArena> In(Guid arenaId);
    }

    public interface IBattleWithArena
    {
        Result<IBattleWithAttacker> WithAttacker(Guid attackerId, Pokemon attackerPokemon);
    }

    public interface IBattleWithAttacker
    {
        Result<Battle> WithDefender(Guid defenderId, Pokemon defenderPokemon);
    }
}