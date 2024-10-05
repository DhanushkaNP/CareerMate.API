using CareerMate.Models.Entities.Coordinators;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CareerMate.Infrastructure.Persistence.EntityConfigurations
{
    public class CoordinatorConfigurations : IEntityTypeConfiguration<Coordinator>
    {
        public void Configure(EntityTypeBuilder<Coordinator> builder)
        {
            builder.ToTable(nameof(Coordinator));

            builder.HasKey(i => i.Id);

            builder.HasOne(i => i.Faculty)
                .WithMany(i => i.Coordinators)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(i => i.ApplicationUser)
                .WithOne(i => i.Coordinator)
                .HasForeignKey<Coordinator>(i => i.ApplicationUserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
