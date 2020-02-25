using Pomelo.Kernel.Events.Abstractions;

namespace Pokens.Training.Business
{
    internal sealed class TrainerCollectedExperienceEvent : IIntegrationEvent
    {
        private TrainerCollectedExperienceEvent()
        {
        }

        public string PokemonId { get; private set; }

        public int Amount { get; private set; }
    }
}