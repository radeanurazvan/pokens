using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pokens.Trainers.Domain;

namespace Pokens.Trainers.Persistence.EntityFramework.Configurations
{
    internal sealed class TrainerConfiguration : IEntityTypeConfiguration<Trainer>
    {
        public void Configure(EntityTypeBuilder<Trainer> builder)
        {
            builder.ToTable("Trainers");
            builder.HasKey(t => t.Id);

            builder.HasOne(t => t.User)
                .WithOne()
                .HasPrincipalKey<User>(u => u.Id)
                .HasForeignKey<Trainer>(t => t.UserId);
        }
    }
}