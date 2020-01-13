using Pokens.Pokedex.Domain;
using Pomelo.Kernel.Messaging.Abstractions;
using System.Collections.Generic;
using System.Linq;

namespace Pokens.Pokedex.Business
{
    internal sealed class PokemonImagesChanged : IBusMessage
    {
        private PokemonImagesChanged()
        {
        }

        public PokemonImagesChanged(Pokemon pokemon)
            : this()
        {
            PokemonId = pokemon.Id;
            Images = pokemon.Images.Select(i => new ChangedImage(i));
        }

        public string PokemonId { get; private set; }

        public IEnumerable<ChangedImage> Images{ get; private set; }

        internal sealed class ChangedImage
        {
            public ChangedImage(Image image)
            {
                Name = image.ImageName;
                Content = image.ContentImage;
            }

            public string Name { get; private set; }

            public byte[] Content { get; private set; }
        }
    }
}