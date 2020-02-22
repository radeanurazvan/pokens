using Pokens.Pokedex.Domain;
using System.Collections.Generic;
using System.Linq;
using Pomelo.Kernel.Events.Abstractions;

namespace Pokens.Pokedex.Domain
{
    internal sealed class PokemonImagesChanged : IDomainEvent
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