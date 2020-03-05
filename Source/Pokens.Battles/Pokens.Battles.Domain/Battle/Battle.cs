using System;
using System.Collections.Generic;
using System.Linq;
using CSharpFunctionalExtensions;
using Pokens.Battles.Resources;
using Pomelo.Kernel.Common;
using Pomelo.Kernel.Domain;

namespace Pokens.Battles.Domain
{
    public sealed class Battle : AggregateRoot, IIdentifiedBattle, IBattleWithArena, IBattleWithAttacker
    {
        private readonly ICollection<BattleTurn> turns = new List<BattleTurn>();

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
                .Tap(() => AttackerPokemon = new PokemonInFight(attackerPokemon.Id, attackerPokemon.Stats.Defensive, attackerPokemon.Stats.Offensive))
                .Map(() => this as IBattleWithAttacker);
        }

        public Result<Battle> WithDefender(Guid defenderId, Pokemon defenderPokemon)
        {
            var trainerResult = Result.FailureIf(defenderId == Guid.Empty, Messages.InvalidTrainer);
            var pokemonResult = defenderPokemon.EnsureExists(Messages.InvalidPokemon)
                .Ensure(p => p.TrainerId == defenderId, Messages.TrainerDoesNotOwnPokemon);

            return Result.FirstFailureOrSuccess(trainerResult, pokemonResult)
                .Tap(() => DefenderId = defenderId)
                .Tap(() => DefenderPokemon = new PokemonInFight(defenderPokemon.Id, defenderPokemon.Stats.Defensive, defenderPokemon.Stats.Offensive))
                .Tap(() => ReactToDomainEvent(new BattleStartedEvent(this)))
                .Map(() => this);
        }

        public Guid ArenaId { get; private set; }

        public Guid AttackerId { get; private set; }

        internal PokemonInFight AttackerPokemon { get; private set; }
        
        public Guid DefenderId { get; private set; }

        internal PokemonInFight DefenderPokemon { get; private set; }

        public DateTime StartedAt { get; private set; }

        public Maybe<DateTime> EndedAt { get; private set; } = Maybe<DateTime>.None;

        public bool IsOnGoing => !HasEnded;

        public bool HasEnded => EndedAt.HasValue;

        public Maybe<Guid> Winner { get; private set; } = Maybe<Guid>.None;
        
        public Maybe<Guid> Loser { get; private set; } = Maybe<Guid>.None;

        private IEnumerable<Guid> Participants => new List<Guid> { AttackerId, DefenderId };

        public Guid ActivePlayer => Participants.ElementAt(turns.Count % Participants.Count());
        
        public Guid WaitingPlayer => Participants.ElementAt((turns.Count + 1) % Participants.Count());

        private IEnumerable<PokemonInFight> FightingPokemons => new List<PokemonInFight> { AttackerPokemon, DefenderPokemon };

        internal PokemonInFight ActivePokemon => FightingPokemons.ElementAt(turns.Count % FightingPokemons.Count());
        
        private PokemonInFight WaitingPokemon => FightingPokemons.ElementAt((turns.Count + 1) % FightingPokemons.Count());

        internal Result TakeTurn(Trainer player, Ability ability)
        {
            var playerResult = player.EnsureExists(Messages.InvalidTrainer);
            var abilityResult = ability.EnsureExists(Messages.InvalidAbility);

            var damageResult = ComputeDamageFor(ability);
            return Result.FirstFailureOrSuccess(playerResult, abilityResult, damageResult)
                .Ensure(() => IsOnGoing, Messages.BattleAlreadyEnded)
                .Ensure(() => ActivePlayer == player.Id, Messages.YouAreNotTheActivePlayer)
                .Ensure(() => ActivePokemon.CanUse(ability), Messages.AbilityIsOnCooldown)
                .Tap(() => ReactToDomainEvent(new PlayerUsedAbilityEvent(ActivePlayer, ability, damageResult.Value)))
                .Tap(() => ActivePokemon.Cooldowns.Select(c => new PlayerCooldownChangedEvent(Id, ActivePlayer, c.AbilityId, c.Left)).ForEach(a => AddDomainEvent(a)))
                .TapIf(damageResult.IsSuccess && damageResult.Value == 0, () => AddDomainEvent(new PokemonDodgedAbilityEvent(Id, WaitingPlayer)))
                .TapIf(damageResult.IsSuccess && damageResult.Value != 0, () => AddDomainEvent(new BattleHealthChangedEvent(Id, WaitingPlayer, WaitingPokemon.Defensive.Health)))
                .TapIf(WaitingPokemon.HasFainted, ConcludeBattle)
                .Tap(() => ReactToDomainEvent(new PlayerTookTurnEvent(Id, ActivePlayer, ability.Id)))
                .Tap(() => AddDomainEvent(new ActivePlayerChangedEvent(this)));
        }

        private void ConcludeBattle()
        {
            ReactToDomainEvent(new BattleEndedEvent(this));
        }

        private Result<int> ComputeDamageFor(Ability ability)
        {
            if (ability == null)
            {
                return Result.Failure<int>(Messages.InvalidAbility);
            }

            var hasDodged = Rate.Create(WaitingPokemon.Defensive.DodgeChance).Test();
            if (hasDodged)
            {
                return Result.Ok(0);
            }

            var isCriticalStrike = Rate.Create(ActivePokemon.Offensive.CriticalStrikeChance).Test();
            if (isCriticalStrike)
            {
                return Result.Ok(2 * ability.Damage);
            }

            return Result.Ok(ability.Damage);
        }

        private void When(BattleStartedEvent @event)
        {
            Id = @event.Id;
            ArenaId = @event.ArenaId;
            AttackerId = @event.AttackerId;

            var attackerDefensive = new DefensiveStats(@event.AttackerPokemon.Health, @event.AttackerPokemon.Defense, @event.AttackerPokemon.DodgeChange);
            var attackerOffensive = new OffensiveStats(@event.AttackerPokemon.AttackPower, @event.AttackerPokemon.CriticalStrikeChance);
            AttackerPokemon = new PokemonInFight(@event.AttackerPokemon.Id, attackerDefensive, attackerOffensive);
            DefenderId = @event.DefenderId;

            var defenderDefensive = new DefensiveStats(@event.DefenderPokemon.Health, @event.DefenderPokemon.Defense, @event.DefenderPokemon.DodgeChange);
            var defenderOffensive = new OffensiveStats(@event.DefenderPokemon.AttackPower, @event.DefenderPokemon.CriticalStrikeChance);
            DefenderPokemon = new PokemonInFight(@event.DefenderPokemon.Id, defenderDefensive, defenderOffensive);
            StartedAt = @event.StartedAt;
        }

        private void When(PlayerUsedAbilityEvent @event)
        {
            ActivePokemon.DecrementCooldowns();
            ActivePokemon.Use(@event.Ability);
            WaitingPokemon.TakeHit(@event.DamageDealt);
        }

        private void When(PlayerTookTurnEvent @event)
        {
            turns.Add(new BattleTurn(ActivePlayer, @event.AbilityId, @event.TakenAt));
        }

        private void When(BattleEndedEvent @event)
        {
            Winner = @event.Winner;
            Loser = @event.Loser;
            EndedAt = @event.EndedAt;
        }

        private void When(PokemonDodgedAbilityEvent @event)
        {
        }

        private void When(BattleHealthChangedEvent @event)
        {
        }

        private void When(ActivePlayerChangedEvent @event)
        {
        }

        private void When(PlayerCooldownChangedEvent @event)
        {
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

    internal sealed class BattleTurn
    {
        private BattleTurn()
        {
        }

        public BattleTurn(Guid playerId, Guid abilityId, DateTime takenAt)
        {
            PlayerId = playerId;
            AbilityId = abilityId;
            TakenAt = takenAt;
        }

        public Guid PlayerId { get; private set; }

        public Guid AbilityId { get; private set; }

        public DateTime TakenAt { get; private set; }
    }
}