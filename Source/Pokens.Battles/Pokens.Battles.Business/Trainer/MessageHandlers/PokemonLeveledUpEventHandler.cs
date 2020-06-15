using System;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using Pokens.Battles.Domain;
using Pokens.Battles.Resources;
using Pomelo.Kernel.Domain;
using Pomelo.Kernel.Events.Abstractions;

namespace Pokens.Battles.Business
{
    internal sealed class PokemonLeveledUpEventHandler : IIntegrationEventHandler<PokemonLeveledUpEvent>
    {
        private readonly IRepositoryMediator mediator;
        private readonly ILogger logger;

        public PokemonLeveledUpEventHandler(IRepositoryMediator mediator, ILogger<PokemonLeveledUpEventHandler> logger)
        {
            this.mediator = mediator;
            this.logger = logger;
        }

        public Task Handle(IntegrationEvent<PokemonLeveledUpEvent> @event)
        {
            return mediator.ReadById<Trainer>(@event.Metadata.AggregateId).ToResult(Messages.TrainerNotFound)
                .Tap(t => t.RaisePokemonLevel(@event.Data.PokemonId, @event.Data.Level))
                .Tap(() => mediator.Write<Trainer>().Save())
                .OnFailure(e => this.logger.LogError(e))
                .OnFailure(e => throw new InvalidOperationException(e));
        }
    }

    internal sealed class PokemonHealthLeveledUpEventHandler : IIntegrationEventHandler<PokemonLeveledUpEvent>
    {
        private readonly IRepositoryMediator mediator;

        public PokemonHealthLeveledUpEventHandler(IRepositoryMediator mediator)
        {
            this.mediator = mediator;
        }

        public Task Handle(IntegrationEvent<PokemonLeveledUpEvent> @event)
        {
            return mediator.ReadById<Trainer>(@event.Metadata.AggregateId).ToResult(Messages.TrainerNotFound)
                .Tap(t => t.RaisePokemonHealth(@event.Data.PokemonId, @event.Data.Level))
                .Tap(() => mediator.Write<Trainer>().Save());
        }
    }
}