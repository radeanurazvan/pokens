using System;
using CSharpFunctionalExtensions;
using Pokens.Training.Resources;
using Pomelo.Kernel.Common;
using Pomelo.Kernel.Domain;

namespace Pokens.Training.Domain
{
    public sealed class StarterPokemon : DocumentEntity
    {
        private StarterPokemon()
        {
        }

        private StarterPokemon(string id)
            : this()
        {
            Id = id;
        }

        public static Result<StarterPokemon> Create(string id)
        {
            return new Guid(id).EnsureNotEmpty(Messages.InvalidId)
                .Map(_ => new StarterPokemon(id));
        }
    }
}