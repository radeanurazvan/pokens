using Pomelo.Kernel.Messaging.Abstractions;

namespace Pokens.Training.Business
{
    internal sealed class PokemonStarterChanged : IBusMessage
    {
        private PokemonStarterChanged()
        {
        }

        public string PokemonId { get; private set; }

        public bool PokemonIsStarter { get; private set; }
    }
}