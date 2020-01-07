using Pokens.Training.Domain;
using Pomelo.Kernel.Messaging.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pokens.Training.Business
{
    internal sealed class PokemonImagesChanged : IBusMessage
    {
        private PokemonImagesChanged()
        {
        }

        public string PokemonId { get; private set; }

        public ICollection<Image> Images { get; private set; }
    }
}
