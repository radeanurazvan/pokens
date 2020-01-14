using System;
using System.Collections.Generic;
using MediatR;

namespace Pokens.Training.Business
{
    public sealed class GetTrainersPokemonsQuery : IRequest<IEnumerable<PokemonModel>>
    {
        public GetTrainersPokemonsQuery(IEnumerable<string> trainerIds)
        {
            TrainerIds = trainerIds;
        }

        public IEnumerable<string> TrainerIds { get; }
    }
}