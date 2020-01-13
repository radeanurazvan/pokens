using System;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using EnsureThat;
using Microsoft.Extensions.Logging;
using Pokens.Training.Domain;
using Pokens.Training.Resources;
using Pomelo.Kernel.Common;
using Pomelo.Kernel.Domain;
using Pomelo.Kernel.Messaging.Abstractions;

namespace Pokens.Training.Business
{
    internal sealed class PokemonStarterChangedHandler : IBusMessageHandler<PokemonStarterChanged>
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

        public Task Handle(PokemonStarterChanged message)
        {
            EnsureArg.IsNotNull(message);
            return this.repository.FindOne<PokemonDefinition>(d => d.Id == message.PokemonId).ToResult(Messages.PokemonNotFound)
                .Tap(d => d.ChangeIsStarter(message.PokemonIsStarter))
                .Tap(d => this.repository.Update(d))
                .OnFailure(e => this.logger.LogError($"Integrating change starter failed with error {e} for message {message.ToJson()}"))
                .OnFailure(e => throw new Exception(e));
        }
    }
}