using System;

namespace Pokens.Pokedex.Domain
{
    public abstract class PokedexEntity
    {
        protected PokedexEntity()
        {
            Id = Guid.NewGuid().ToString();
        }

        public string Id { get; private set; }
    }
}