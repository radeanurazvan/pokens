using System;
using Pomelo.Kernel.Common;

namespace Pokens.Battles.Business
{
    public sealed class ChallengeTrainerCommand : ICommand
    {
        public ChallengeTrainerCommand(Guid arenaId, Guid challengerId, Guid challengerPokemonId, Guid challengedId, Guid challengedPokemonId)
        {
            ArenaId = arenaId;
            ChallengerId = challengerId;
            ChallengerPokemonId = challengerPokemonId;
            ChallengedId = challengedId;
            ChallengedPokemonId = challengedPokemonId;
        }

        public Guid ArenaId { get; }

        public Guid ChallengerId { get; }
        
        public Guid ChallengerPokemonId { get; }

        public Guid ChallengedId { get; }

        public Guid ChallengedPokemonId { get; }
    }
}