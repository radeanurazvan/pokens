using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using EnsureThat;
using MediatR;
using Pokens.Training.Domain;
using Pomelo.Kernel.Domain;

namespace Pokens.Training.Business
{
    internal sealed class GetMyPokemonsQueryHandler : IRequestHandler<GetMyPokemonsQuery, Maybe<IEnumerable<PokemonModel>>>
    {
        private readonly ICollectionRepository repository;

        public GetMyPokemonsQueryHandler(ICollectionRepository repository)
        {
            this.repository = repository;
        }

        public async Task<Maybe<IEnumerable<PokemonModel>>> Handle(GetMyPokemonsQuery request, CancellationToken cancellationToken)
        {
            EnsureArg.IsNotNull(request);
            var trainerOrNothing = await repository.FindOne<Trainer>(t => t.Id == request.TrainerId);
            return trainerOrNothing
                .Select(t => t.CaughtPokemons)
                .Select(p => p.Select(x => new PokemonModel(request.TrainerId, x)));
        }
    }
}