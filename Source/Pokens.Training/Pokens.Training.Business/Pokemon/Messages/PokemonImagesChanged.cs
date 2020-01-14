using Pomelo.Kernel.Messaging.Abstractions;
using System.Collections.Generic;

namespace Pokens.Training.Business
{
    internal sealed class PokemonImagesChanged : IBusMessage
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
