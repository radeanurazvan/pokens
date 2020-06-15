using System;
using System.Collections.Generic;
using System.Linq;
using Pomelo.Kernel.DataSynchronization;

namespace Pokens.Battles.Read.Domain
{
    public sealed class ArenaModel : SynchronizationModel
    {
        public string Name { get; set; }

        public int RequiredLevel { get; set; }

        public ICollection<ArenaTrainerModel> Trainers { get; set; } = new List<ArenaTrainerModel>();

        public bool HasTrainer(string trainerId) => Trainers.Any(t => t.Id == trainerId);
    }

    public sealed class ArenaTrainerModel 
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public DateTime JoinedAt { get; set; }
    }
}