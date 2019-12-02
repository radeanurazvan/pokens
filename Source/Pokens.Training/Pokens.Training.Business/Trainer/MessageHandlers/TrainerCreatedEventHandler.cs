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
    internal sealed class TrainerCreatedEventHandler : IBusMessageHandler<IntegrationEvent<TrainerCreatedEvent>>
    {
        private readonly ICollectionRepository repository;
        private readonly ILogger logger;

        public TrainerCreatedEventHandler(ICollectionRepository repository, ILogger<TrainerCreatedEventHandler> logger)
        {
            EnsureArg.IsNotNull(repository);
            EnsureArg.IsNotNull(logger);
            this.repository = repository;
            this.logger = logger;
        }

        public Task Handle(IntegrationEvent<TrainerCreatedEvent> message)
        {
            EnsureArg.IsNotNull(message);
            return Task.Run(() => HandleCore(message.Data));
        }

        private void HandleCore(TrainerCreatedEvent message)
        {
            Trainer.Create(message.Id, message.Name)
                .OnFailure(e => this.logger.LogError($"Integrating new trainer failed with error: {e} for message: {message.ToJson()}"))
                .Tap(t => this.repository.Add(t));
        }
    }
}