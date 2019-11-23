using Microsoft.EntityFrameworkCore;
using Pokens.Trainers.Domain;

namespace Pokens.Trainers.Persistence.EntityFramework
{
    internal sealed class TrainersContext : DbContext
    {
        public TrainersContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Trainer> Trainers { get; private set; }

        public DbSet<User> Users { get; private set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TrainersContext).Assembly);
        }
    }
}