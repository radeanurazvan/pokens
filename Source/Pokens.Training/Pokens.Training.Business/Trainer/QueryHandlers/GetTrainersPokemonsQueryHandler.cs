using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EnsureThat;
using MediatR;
using Pokens.Training.Domain;
using Pomelo.Kernel.Domain;

namespace Pokens.Training.Business
{
    internal sealed class GetTrainersPokemonsQueryHandler : IRequestHandler<GetTrainersPokemonsQuery, IEnumerable<PokemonModel>>
    {
        private readonly ICollectionRepository repository;

        public GetTrainersPokemonsQueryHandler(ICollectionRepository repository)
        {
            this.repository = repository;
        }

        public async Task<IEnumerable<PokemonModel>> Handle(GetTrainersPokemonsQuery request, CancellationToken cancellationToken)
        {
            EnsureArg.IsNotNull(request);
            var trainers = await repository.Find<Trainer>(t => request.TrainerIds.Contains(t.Id));
            return trainers
                .SelectMany(t => t.CaughtPokemons.Select(cp => new PokemonModel(t.Id, cp)));
        }
    }
}