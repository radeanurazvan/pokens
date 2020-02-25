using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Pokens.Training.Domain;
using Pokens.Training.Resources;
using Pomelo.Kernel.Domain;
using Pomelo.Kernel.Events.Abstractions;

namespace Pokens.Training.Business
{
    internal sealed class TrainerCollectedExperienceEventHandler : IIntegrationEventHandler<TrainerCollectedExperienceEvent>
    {
        private readonly ICollectionRepository repository;

        public TrainerCollectedExperienceEventHandler(ICollectionRepository repository)
        {
            this.repository = repository;
        }

        public Task Handle(IntegrationEvent<TrainerCollectedExperienceEvent> @event)
        {
            return repository.FindOne<Trainer>(t => t.Id == @event.Metadata.AggregateId.ToString()).ToResult(Messages.TrainerNotFound)
                .Bind(t => t.CollectExperience(@event.Data.PokemonId, @event.Data.Amount))
                .Tap(() => repository.Commit());
        }
    }
}