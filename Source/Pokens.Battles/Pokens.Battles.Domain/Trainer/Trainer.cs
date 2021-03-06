﻿using System;
using System.Collections.Generic;
using System.Linq;
using CSharpFunctionalExtensions;
using Pokens.Battles.Resources;
using Pomelo.Kernel.Common;
using Pomelo.Kernel.Domain;
using Pomelo.Kernel.Events.Abstractions;

namespace Pokens.Battles.Domain
{
    public sealed class Trainer : AggregateRoot
    {
        private readonly ICollection<Pokemon> pokemons = new List<Pokemon>();
        private readonly ICollection<Challenge> challenges = new List<Challenge>();
        private readonly ICollection<TrainerBattle> battles = new List<TrainerBattle>();

        private Trainer()
        {
        }

        private Trainer(Guid id , string name)
            : this()
        {
            ReactToDomainEvent(new TrainerRegisteredEvent(id, name));
        }

        public static Trainer Register(Guid id, string name)
        {
            return new Trainer(id, name);
        }

        public string Name { get; private set; }

        public Maybe<Guid> Enrollment { get; private set; } = Maybe<Guid>.None;

        public bool IsAutoMode { get; private set; } = false;

        public bool IsEnrolled => Enrollment.HasValue;

        public bool IsEnrolledIn(Arena arena) => IsEnrolledIn(arena.Id);
        
        public bool IsEnrolledIn(Guid arenaId) => IsEnrolled && Enrollment == arenaId;

        public IEnumerable<Challenge> Challenges => this.challenges;

        public IEnumerable<Pokemon> Pokemons => this.pokemons;

        public IEnumerable<TrainerBattle> Battles => this.battles;

        public Maybe<TrainerBattle> CurrentBattle => this.battles.TryFirst(b => b.EndedAt.HasNoValue);

        public void ToggleAutoMode()
        {
            var @event = IsAutoMode ? new TrainerDisabledAutoModeEvent() : (IDomainEvent)new TrainerActivatedAutoModeEvent();
            ReactToDomainEvent(@event);
        }

        public void Catch(Pokemon pokemon)
        {
            ReactToDomainEvent(new TrainerCaughtPokemonEvent(pokemon));
        }

        internal int EnrollmentLevel => pokemons.Select(p => p.Level).Concat(new List<int>{0}).Max();

        public Result EnrollIn(Arena arena)
        {
            return Result.FailureIf(Enrollment.HasValue, Messages.TrainerAlreadyEnrolled)
                .Tap(() => ReactToDomainEvent(new TrainerEnrolledEvent(arena.Id)));
        }

        public Result UseAbilityIn(Battle battle, Guid abilityId)
        {
            var pokemonResult = battle.EnsureExists(Messages.BattleNotFound)
                .Bind(_ => battles.TryFirst(b => b.Id == battle.Id).ToResult(Messages.BattleNotFound))
                .Bind(b => pokemons.TryFirst(p => p.Id == b.Pokemon).ToResult(Messages.InvalidPokemon));
            return pokemonResult
                .Bind(p => p.Abilities.TryFirst(a => a.Id == abilityId).ToResult(Messages.InvalidAbility))
                .Ensure(a => a.RequiredLevel <= pokemonResult.Value.Level, Messages.AbilityRequiresLevel)
                .Bind(a => battle.TakeTurn(this, a));
        }

        public Result AcknowledgeWonBattle(Guid battleId, int experience)
        {
            return battles.TryFirst(b => b.Id == battleId).ToResult(Messages.BattleNotFound)
                .Tap(b => AddDomainEvent(new TrainerCollectedExperienceEvent(b.Pokemon, experience)))
                .Tap(() => ReactToDomainEvent(new TrainerWonBattleEvent(Id, battleId, experience)));
        }

        public Result AcknowledgeLostBattle(Guid battleId, int experience)
        {
            return battles.TryFirst(b => b.Id == battleId).ToResult(Messages.BattleNotFound)
                .Tap(b => AddDomainEvent(new TrainerCollectedExperienceEvent(b.Pokemon, experience)))
                .Tap(() => ReactToDomainEvent(new TrainerLostBattleEvent(Id, battleId, experience)));
        }

        public void RaisePokemonLevel(Guid pokemonId, int newLevel)
        {
            this.pokemons.TryFirst(p => p.Id == pokemonId)
                .Execute(p => ReactToDomainEvent(new TrainerPokemonChangedLevelEvent(pokemonId, newLevel)));
        }

        public void RaisePokemonHealth(Guid pokemonId, int newLevel)
        {
            this.pokemons.TryFirst(p => p.Id == pokemonId)
                .Execute(_ => ReactToDomainEvent(new TrainerPokemonHealthLevelChangedEvent(pokemonId, newLevel)));
        }

        internal Result LeaveArena()
        {
            return Result.SuccessIf(Enrollment.HasValue, Messages.TrainerIsNotEnrolled)
                .Ensure(() => CurrentBattle.HasNoValue, Messages.CannotLeaveWhileInBattle)
                .Tap(() => ReactToDomainEvent(new TrainerLeftArenaEvent()));
        }

        internal Result<Guid> Challenge(Trainer challenged, Guid challengerPokemonId, Guid challengedPokemonId)
        {
            var challengeId = Guid.NewGuid();
            var hasAlreadyChallenged = challenges
                .Where(c => c.IsPending)
                .Where(c => c.IsNotExpired)
                .Any(c => c.HasParticipants(this, challenged));

            var challengerPokemonResult = this.pokemons.TryFirst(p => p.Id == challengerPokemonId).ToResult(Messages.TrainerDoesNotOwnPokemon); 
            var challengedPokemonResult = challenged.Pokemons.TryFirst(p => p.Id == challengedPokemonId).ToResult(Messages.TrainerDoesNotOwnPokemon); 

            return Result.FailureIf(hasAlreadyChallenged, Messages.TrainerAlreadyChallenged)
                .Ensure(() => this != challenged, Messages.CannotChallengeSelf)
                .Ensure(() => this.HasPokemon(challengerPokemonId) && challenged.HasPokemon(challengedPokemonId), Messages.TrainerDoesNotOwnPokemon)
                .Tap(() => ReactToDomainEvent(new TrainerChallengedEvent(challenged, challengeId, challengerPokemonResult.Value, challengedPokemonResult.Value)))
                .Tap(() => challenged.ReceiveChallengeFrom(this, challengeId, challengedPokemonResult.Value, challengerPokemonResult.Value))
                .Map(() => challengeId);
        }

        internal Result AcceptChallenge(Trainer challenger, Challenge challenge)
        {
            var challengerResult = challenger.EnsureExists(Messages.InvalidTrainer);
            var challengeResult = this.challenges.TryFirst(c => c == challenge).ToResult(Messages.ChallengeNotFound)
                .Ensure(c => c.ChallengedId == this.Id, Messages.ChallengeNotFound)
                .Ensure(c => c.ChallengerId == challenger.Id, Messages.ChallengeNotFound)
                .Ensure(c => c.IsNotExpired, Messages.ChallengeExpired)
                .Ensure(c => c.IsPending, Messages.ChallengeAlreadyAnswered)
                .Ensure(_ => this.IsEnrolled, Messages.ArenaAlreadyLeft)
                .Ensure(_ => challenger.IsEnrolled, Messages.ArenaAlreadyLeft)
                .Ensure(c => Enrollment.Value == c.ArenaId, Messages.ArenaAlreadyLeft)
                .Ensure(c => challenger.Enrollment.Value == c.ArenaId, Messages.ArenaAlreadyLeft);

            return Result.FirstFailureOrSuccess(challengerResult, challengeResult)
                .Ensure(() => challenger.CurrentBattle.HasNoValue, Messages.TrainerAlreadyInBattle)
                .Ensure(() => this.CurrentBattle.HasNoValue, Messages.TrainerAlreadyInBattle)
                .Tap(() => ReactToDomainEvent(new TrainerAcceptedChallengeEvent(Enrollment.Unwrap(), challenge.Id)))
                .Tap(() => challenger.ReactToDomainEvent(TrainerChallengeGotAnsweredEvent.AcceptedFor(challenge.Id)));
        }

        public Result RejectChallenge(Guid challengeId)
        {
            var challengeResult = this.challenges.TryFirst(c => c.Id == challengeId).ToResult(Messages.ChallengeNotFound)
                .Ensure(c => c.ChallengedId == this.Id, Messages.ChallengeNotFound)
                .Ensure(c => c.IsPending, Messages.ChallengeAlreadyAnswered);

            return Result.FirstFailureOrSuccess(challengeResult)
                .Tap(() => ReactToDomainEvent(TrainerChallengeGotAnsweredEvent.RejectedFor(challengeId)));
        }

        internal Result StartBattleAgainst(Trainer enemy, Guid challengeId)
        {
            var challengeResult = this.challenges.TryFirst(c => c.Id == challengeId)
                .ToResult(Messages.TrainerHasNotAcceptedChallenge);
            var enemyResult = enemy.EnsureExists(Messages.InvalidTrainer);

            return Result.FirstFailureOrSuccess(challengeResult, enemyResult)
                .Ensure(() => CurrentBattle.HasNoValue, Messages.TrainerAlreadyInBattle)
                .Ensure(() => enemy.CurrentBattle.HasNoValue, Messages.TrainerAlreadyInBattle)
                .Bind(() => enemy.EnterBattleAgainst(challengeId))
                .Tap(() => ReactToDomainEvent(new TrainerStartedBattleEvent(challengeResult.Value)));
        }

        private Result EnterBattleAgainst(Guid challengeId)
        {
            return this.challenges.TryFirst(c => c.Id == challengeId)
                .ToResult(Messages.TrainerHasNotAcceptedChallenge)
                .Bind(c => ReactToDomainEvent(new TrainerEnteredBattleEvent(c)));
        }

        private void ReceiveChallengeFrom(Trainer challenger, Guid challengeId, Pokemon challengedPokemon, Pokemon challengerPokemon) 
            => ReactToDomainEvent(new TrainerHasBeenChallengedEvent(challenger, challengeId, challengerPokemon, challengedPokemon));

        private bool HasPokemon(Guid id) => this.pokemons.Any(p => p.Id == id);

        private void When(TrainerRegisteredEvent @event)
        {
            Id = @event.Id;
            Name = @event.Name;
        }

        private void When(TrainerCaughtPokemonEvent @event)
        {
            pokemons.Add(@event.Pokemon);
        }

        private void When(TrainerEnrolledEvent @event)
        {
            Enrollment = @event.ArenaId;
        }

        private void When(TrainerLeftArenaEvent @event)
        {
            Enrollment = Maybe<Guid>.None;
        }

        private void When(TrainerChallengedEvent @event)
        {
            var challenge = Domain.Challenge.For(@event.ChallengeId, @event.TrainerId, @event.ChallengedPokemonId)
                .By(this.Id, @event.PokemonId)
                .On(Enrollment.Unwrap())
                .At(@event.ChallengedAt);
            challenges.Add(challenge);
        }

        private void When(TrainerHasBeenChallengedEvent @event)
        {
            var challenge = Domain.Challenge.For(@event.ChallengeId, this.Id, @event.PokemonId)
                .By(@event.ChallengerId, @event.ChallengerPokemonId)
                .On(Enrollment.Unwrap())
                .At(@event.ChallengedAt);
            challenges.Add(challenge);
        }

        private void When(TrainerAcceptedChallengeEvent @event)
        {
            challenges.TryFirst(c => c.Id == @event.ChallengeId)
                .Execute(c => c.MarkAsAccepted());
        }

        private void When(TrainerChallengeGotAnsweredEvent @event)
        {
            challenges.TryFirst(c => c.Id == @event.ChallengeId).ToResult(Messages.ChallengeNotFound)
                .TapIf(@event.Accepted, c => c.MarkAsAccepted())
                .TapIf(@event.Rejected, c => c.MarkAsRejected());
        }

        private void When(TrainerStartedBattleEvent @event)
        {
            this.challenges.TryFirst(c => c.HasParticipants(this.Id, @event.EnemyId))
                .Execute(c => c.MarkAsHonored());
            this.battles.Add(TrainerBattle.Create(@event.ChallengeId, @event.EnemyId, @event.PokemonId));
        }

        private void When(TrainerEnteredBattleEvent @event)
        {
            this.challenges.TryFirst(c => c.HasParticipants(this.Id, @event.EnemyId))
                .Execute(c => c.MarkAsHonored());
            this.battles.Add(TrainerBattle.Create(@event.ChallengeId, @event.EnemyId, @event.PokemonId));
        }

        private void When(TrainerLostBattleEvent @event)
        {
            this.battles.TryFirst(b => b.Id == @event.BattleId)
                .Execute(b => b.MarkEnding(@event.LostAt));
        }

        private void When(TrainerWonBattleEvent @event)
        {
            this.battles.TryFirst(b => b.Id == @event.BattleId)
                .Execute(b => b.MarkEnding(@event.WonAt));
        }

        private void When(TrainerPokemonChangedLevelEvent @event)
        {
            this.pokemons.TryFirst(p => p.Id == @event.PokemonId).Execute(p => p.GoToLevel(@event.Level));
        }

        private void When(TrainerActivatedAutoModeEvent @event)
        {
            IsAutoMode = true;
        }

        private void When(TrainerDisabledAutoModeEvent @event)
        {
            IsAutoMode = false;
        }

        private void When(TrainerCollectedExperienceEvent @event)
        {
        }

        private void When(TrainerPokemonHealthLevelChangedEvent @event)
        {
            this.pokemons.TryFirst(p => p.Id == @event.PokemonId)
                .Execute(p => p.EmbraceHealthBonus(@event.BonusHealth));
        }
    }
}