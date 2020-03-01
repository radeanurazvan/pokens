using System;
using System.Linq;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using EnsureThat;
using Microsoft.Extensions.Logging;
using Pokens.Training.Domain;
using Pomelo.Kernel.Domain;
using Pomelo.Kernel.Events.Abstractions;

namespace Pokens.Training.Business
{
    internal sealed class PokemonCreatedHandler : IIntegrationEventHandler<PokemonCreated>
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

        public Task Handle(IntegrationEvent<PokemonCreated> message)
        {
            EnsureArg.IsNotNull(message);
            var stats = new Stats(message.Data.Health, message.Data.Defense, message.Data.AttackPower, message.Data.CriticalStrikeChance, message.Data.DodgeChance);
            var abilities = message.Data.Abilities.Select(a => new Ability(a.Id, a.Name, a.Description, a.Damage, a.RequiredLevel, a.Cooldown, a.Image));

            return PokemonDefinition.Create(message.Data.Id, message.Data.Name, stats, message.Data.CatchRate, abilities)
                .Tap(p => this.repository.Add(p))
                .OnFailure(e => this.logger.LogError($"Integrating pokemon definition failed with error {e}, for message {message.Data.Id}"))
                .OnFailure(e => throw new Exception(e));
        }
    }
}