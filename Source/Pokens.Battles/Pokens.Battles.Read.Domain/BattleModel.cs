using System;
using System.Collections.Generic;
using Pomelo.Kernel.DataSynchronization;

namespace Pokens.Battles.Read.Domain
{
    public sealed class BattleModel : SynchronizationModel
    {
        public BattleModel()
        {
            Commentaries.Add("Battle started!");
        }

        public string ArenaId { get; set; }

        public string AttackerId { get; set; }

        public string AttackerPokemonId { get; set; }

        public int InitialAttackerHealth { get; set; }

        public int AttackerHealth { get; set; }

        public string DefenderId { get; set; }

        public string DefenderPokemonId { get; set; }

        public int InitialDefenderHealth { get; set; }

        public int DefenderHealth { get; set; }

        public DateTime StartedAt { get; set; }

        public DateTime? EndedAt { get; set; } = null;

        public ICollection<BattleCommentary> Commentaries { get; set; } = new List<BattleCommentary>();
    }

    public sealed class BattleCommentary
    {
        public DateTime At { get; set; }

        public string Body { get; set; }

        public static implicit operator BattleCommentary(string x) => new BattleCommentary
        {
            At = DateTime.UtcNow,
            Body = x
        };
    }
}