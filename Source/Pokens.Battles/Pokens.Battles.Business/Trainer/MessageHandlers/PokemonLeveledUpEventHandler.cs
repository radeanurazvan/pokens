using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Pokens.Battles.Domain;
using Pokens.Battles.Resources;
using Pomelo.Kernel.Domain;
using Pomelo.Kernel.Events.Abstractions;

namespace Pokens.Battles.Business
{
    internal sealed class PokemonLeveledUpEventHandler : IIntegrationEventHandler<PokemonLeveledUpEvent>
    {
        private readonly IRepositoryMediator mediator;

        public PokemonLeveledUpEventHandler(IRepositoryMediator mediator)
        {
            this.mediator = mediator;
        }

        public Task Handle(IntegrationEvent<PokemonLeveledUpEvent> @event)
        {
            return mediator.ReadById<Trainer>(@event.Metadata.AggregateId).ToResult(Messages.TrainerNotFound)
                .Tap(t => t.RaisePokemonLevel(@event.Data.PokemonId, @event.Data.Level))
                .Tap(() => mediator.Write<Trainer>().Save());
        }
    }
}