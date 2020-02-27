using System;
using System.Collections.Generic;
using CSharpFunctionalExtensions;
using Pomelo.Kernel.DataSynchronization;

namespace Pokens.Battles.Read.Domain
{
    public class TrainerModel : SynchronizationModel
    {
        public override string GetCollectionName() => "Trainers";

        public string Name { get; set; }

        public ICollection<ChallengeModel> Challenges { get; set; } = new List<ChallengeModel>();

        public void AcceptedChallenge(string challengeId) => Challenges.TryFirst(c => c.Id == challengeId).Execute(c => c.Status = "Accepted");

        public void ChallengeGotAnswered(string challengeId, bool accepted)
        {
            var status = accepted ? "Accepted" : "Rejected";
            Challenges.TryFirst(c => c.Id == challengeId).Execute(c => c.Status = status);
        }
    }

    public class ChallengeModel
    {
        public string Id { get; set; }

        public string ArenaId { get; set; }

        public string PokemonId { get; set; }

        public string Pokemon { get; set; }

        public string EnemyId { get; set; }

        public string Enemy { get; set; }

        public string EnemyPokemonId { get; set; }

        public string EnemyPokemon { get; set; }

        public string Status { get; set; }

        public string ChallengerId { get; set; }

        public DateTime ChallengedAt { get; set; }
    }
}