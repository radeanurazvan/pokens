using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pokens.Trainers.Domain;

namespace Pokens.Trainers.Persistence.EntityFramework
{
    internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(u => u.Id);

            builder.HasOne(u => u.Trainer)
                .WithOne(t => t.User)
                .HasPrincipalKey<User>(u => u.Id)
                .HasForeignKey<Trainer>(t => t.UserId);
        }
    }
}