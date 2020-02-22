using System.Threading.Tasks;
using EnsureThat;
using Pokens.Battles.Domain;
using Pomelo.Kernel.Domain;
using Pomelo.Kernel.Events.Abstractions;

namespace Pokens.Battles.Business
{
    internal sealed class TrainerCreatedEventHandler : IIntegrationEventHandler<TrainerCreatedEvent>
    {
        private readonly IWriteRepository<Trainer> repository;

        public TrainerCreatedEventHandler(IWriteRepository<Trainer> repository)
        {
            this.repository = repository;
        }

        public async Task Handle(IntegrationEvent<TrainerCreatedEvent> message)
        {
            EnsureArg.IsNotNull(message);

            await this.repository.Add(Trainer.Register(message.Data.Id, message.Data.Name));
            await this.repository.Save();
        }
    }
}