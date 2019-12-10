using System.Collections.Generic;
using CSharpFunctionalExtensions;
using MediatR;

namespace Pokens.Training.Business
{
    public sealed class GetMyPokemonsQuery : IRequest<Maybe<IEnumerable<PokemonModel>>>
    {
        public GetMyPokemonsQuery(string trainerId)
        {
            TrainerId = trainerId;
        }

        public string TrainerId { get; }
    }
}