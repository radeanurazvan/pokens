using Pomelo.Kernel.Events.Abstractions;

namespace Pokens.Training.Business
{
    internal sealed class PokemonStarterChanged : IIntegrationEvent
    {
        private PokemonStarterChanged()
        {
        }

        public string PokemonId { get; private set; }

        public bool PokemonIsStarter { get; private set; }
    }
}