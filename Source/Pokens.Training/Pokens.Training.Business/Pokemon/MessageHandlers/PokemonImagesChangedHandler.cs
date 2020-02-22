using System;
using CSharpFunctionalExtensions;
using EnsureThat;
using Microsoft.Extensions.Logging;
using Pokens.Training.Domain;
using Pokens.Training.Resources;
using Pomelo.Kernel.Domain;
using System.Linq;
using System.Threading.Tasks;
using Pomelo.Kernel.Events.Abstractions;

namespace Pokens.Training.Business
{
    internal sealed class PokemonImagesChangedHandler : IIntegrationEventHandler<PokemonImagesChanged>
    { 
        private readonly ICollectionRepository repository;
        private readonly ILogger logger;

        public PokemonImagesChangedHandler(ICollectionRepository repository, ILogger<PokemonImagesChangedHandler> logger)
        {
            EnsureArg.IsNotNull(repository);
            EnsureArg.IsNotNull(logger);
            this.repository = repository;
            this.logger = logger;
        }

        public Task Handle(IntegrationEvent<PokemonImagesChanged> message)
        {
            EnsureArg.IsNotNull(message);
            return this.repository.FindOne<PokemonDefinition>(d => d.Id == message.Data.PokemonId).ToResult(Messages.PokemonNotFound)
                .Tap(d => d.ChangeImage(message.Data.Images.First().Content))
                .Tap(d => this.repository.Update(d))
                .OnFailure(e => this.logger.LogError($"Integrating change images failed with error {e} for message {message.Metadata.AggregateId}"))
                .OnFailure(e => throw new InvalidOperationException(e));
        }
    }
}
