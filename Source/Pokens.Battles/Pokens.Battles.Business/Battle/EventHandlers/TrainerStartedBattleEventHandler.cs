using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Pokens.Battles.Domain;
using Pokens.Battles.Resources;
using Pomelo.Kernel.Domain;
using Pomelo.Kernel.Events.Abstractions;

namespace Pokens.Battles.Business
{
    internal sealed class TrainerStartedBattleEventHandler : IDomainEventHandler<TrainerStartedBattleEvent>
    {
        private readonly IRepositoryMediator mediator;

        public TrainerStartedBattleEventHandler(IRepositoryMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task Handle(TrainerStartedBattleEvent @event)
        {
            var attackerPokemonResult = await this.mediator.ReadById<Trainer>(@event.TrainerId).ToResult(Messages.TrainerNotFound)
                .Bind(t => t.Pokemons.TryFirst(p => p.Id == @event.PokemonId).ToResult(Messages.TrainerDoesNotOwnPokemon));
            var defenderPokemonResult = await this.mediator.ReadById<Trainer>(@event.EnemyId).ToResult(Messages.TrainerNotFound)
                .Bind(t => t.Pokemons.TryFirst(p => p.Id == @event.EnemyPokemonId).ToResult(Messages.TrainerDoesNotOwnPokemon));

            var writeRepository = this.mediator.Write<Battle>();
            await Result.FirstFailureOrSuccess(attackerPokemonResult, defenderPokemonResult)
                .Bind(() => Battle.FromChallenge(@event.ChallengeId))
                .Bind(b => b.In(@event.ArenaId))
                .Bind(b => b.WithAttacker(@event.TrainerId, attackerPokemonResult.Value))
                .Bind(b => b.WithDefender(@event.EnemyId, defenderPokemonResult.Value))
                .Tap(b => writeRepository.Add(b))
                .Tap(() => writeRepository.Save());
        }
    }
}