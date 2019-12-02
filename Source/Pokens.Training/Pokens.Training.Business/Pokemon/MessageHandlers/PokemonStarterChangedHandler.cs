using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using EnsureThat;
using Microsoft.Extensions.Logging;
using Pokens.Training.Domain;
using Pomelo.Kernel.Common;
using Pomelo.Kernel.Domain;
using Pomelo.Kernel.Messaging.Abstractions;

namespace Pokens.Training.Business
{
    internal sealed class PokemonStarterChangedHandler : IBusMessageHandler<PokemonStarterChanged>
    {
        private readonly ICollectionRepository repository;
        private readonly ILogger logger;

        public PokemonStarterChangedHandler(ICollectionRepository repository, ILogger<PokemonStarterChangedHandler> logger)
        {
            EnsureArg.IsNotNull(repository);
            EnsureArg.IsNotNull(logger);
            this.repository = repository;
            this.logger = logger;
        }

        public Task Handle(PokemonStarterChanged message)
        {
            EnsureArg.IsNotNull(message);
            return Task.Run(() => HandleCore(message));
        }

        private void HandleCore(PokemonStarterChanged message)
        {
            Result.SuccessIf(!message.PokemonIsStarter, "Pokemon is starter")
                .Tap(() => DeleteStarter(message.PokemonId))
                .OnFailureCompensate(() => CreateStarter(message.PokemonId))
                .OnFailure(e => this.logger.LogError($"Integrating starter pokemon failed with error {e} for message {message.ToJson()}"));
        }

        private Result CreateStarter(string id)
        {
            return StarterPokemon.Create(id)
                .Tap(sp => repository.Add(sp));
        }

        private void DeleteStarter(string id)
        {
            repository.Delete<StarterPokemon>(id);
        }
    }
}