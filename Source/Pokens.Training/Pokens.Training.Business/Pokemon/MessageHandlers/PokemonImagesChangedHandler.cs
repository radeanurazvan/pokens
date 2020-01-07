using CSharpFunctionalExtensions;
using EnsureThat;
using Microsoft.Extensions.Logging;
using Pokens.Training.Domain;
using Pokens.Training.Resources;
using Pomelo.Kernel.Common;
using Pomelo.Kernel.Domain;
using Pomelo.Kernel.Messaging.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokens.Training.Business
{
    internal sealed class PokemonImagesChangedHandler : IBusMessageHandler<PokemonImagesChanged>
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

        public Task Handle(PokemonImagesChanged message)
        {
            EnsureArg.IsNotNull(message);
            return Task.Run(() => HandleCore(message));
        }

        private void HandleCore(PokemonImagesChanged message)
        {
            this.repository.FindOne<PokemonDefinition>(d => d.Id == message.PokemonId).ToResult(Messages.PokemonNotFound)
                .Tap(d => d.ChangeImages(message.Images.First()))
                .Tap(d => this.repository.Update(d))
                .OnFailure(e => this.logger.LogError($"Integrating change images failed with error {e} for message {message.ToJson()}"));
        }
    }
}
