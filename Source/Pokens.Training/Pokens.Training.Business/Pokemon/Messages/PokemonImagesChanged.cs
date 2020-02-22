using System.Collections.Generic;
using Pomelo.Kernel.Events.Abstractions;

namespace Pokens.Training.Business
{
    internal sealed class PokemonImagesChanged : IIntegrationEvent
    {
        private PokemonImagesChanged()
        {
        }

        public string PokemonId { get; private set; }

        public IEnumerable<ChangedImage> Images { get; private set; }

        internal sealed class ChangedImage
        {
            private ChangedImage()
            {
            }

            public string Name { get; private set; }

            public byte[] Content { get; private set; }
        }
    }
}
