using System;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using EnsureThat;
using Microsoft.Extensions.Logging;
using Pokens.Training.Domain;
using Pokens.Training.Resources;
using Pomelo.Kernel.Domain;
using Pomelo.Kernel.Events.Abstractions;

namespace Pokens.Training.Business
{
    internal sealed class PokemonStarterChangedHandler : IIntegrationEventHandler<PokemonStarterChanged>
    {
        private readonly ICollectionRepository repository;
        private readonly ILogger logger;

        public PokemonStarterChangedHandler(ICollectionRepository repository, ILogger<PokemonStarterChangedHandler> logger)
        {
            EnsureArg.IsNotNull(repository);
            EnsureArg.IsNotNull(logger);
            this.repository = repository;
            this.logger = logger;
        }

        public Task Handle(IntegrationEvent<PokemonStarterChanged> message)
        {
            EnsureArg.IsNotNull(message);
            return this.repository.FindOne<PokemonDefinition>(d => d.Id == message.Data.PokemonId).ToResult(Messages.PokemonNotFound)
                .Tap(d => d.ChangeIsStarter(message.Data.PokemonIsStarter))
                .Tap(d => this.repository.Update(d))
                .OnFailure(e => this.logger.LogError($"Integrating change starter failed with error {e} for message {message.Metadata.AggregateId}"))
                .OnFailure(e => throw new Exception(e));
        }
    }
}