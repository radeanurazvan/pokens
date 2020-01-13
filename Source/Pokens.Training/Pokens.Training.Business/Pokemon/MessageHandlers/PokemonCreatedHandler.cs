using System;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using EnsureThat;
using Microsoft.Extensions.Logging;
using Pokens.Training.Domain;
using Pomelo.Kernel.Common;
using Pomelo.Kernel.Domain;
using Pomelo.Kernel.Messaging.Abstractions;

namespace Pokens.Training.Business
{
    internal sealed class PokemonCreatedHandler : IBusMessageHandler<PokemonCreated>
    {
        private readonly ICollectionRepository repository;
        private readonly ILogger logger;

        public PokemonCreatedHandler(ICollectionRepository repository, ILogger<PokemonCreatedHandler> logger)
        {
            EnsureArg.IsNotNull(repository);
            EnsureArg.IsNotNull(logger);
            this.repository = repository;
            this.logger = logger;
        }

        public Task Handle(PokemonCreated message)
        {
            EnsureArg.IsNotNull(message);
            var stats = new Stats(message.Health, message.Defense, message.AttackPower, message.CriticalStrikeChance, message.DodgeChance);

            return PokemonDefinition.Create(message.Id, message.Name, stats, message.CatchRate)
                .Tap(p => this.repository.Add(p))
                .OnFailure(e => this.logger.LogError($"Integrating pokemon definition failed with error {e}, for message {message.ToJson()}"))
                .OnFailure(e => throw new Exception(e));
        }
    }
}