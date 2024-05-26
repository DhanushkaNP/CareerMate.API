using CareerMate.Models.Entities.CoordinatorAssistants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CareerMate.Infrastructure.Persistence.EntityConfigurations
{
    public class CoordinatorAssistantConfigurations : IEntityTypeConfiguration<CoordinatorAssistant>
    {
        public void Configure(EntityTypeBuilder<CoordinatorAssistant> builder)
        {
            builder.ToTable(nameof(CoordinatorAssistant));

            builder.HasKey(i => i.Id);

            builder.HasOne(i => i.Faculty)
                .WithMany(i => i.CoordinatorsAssistants)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(i => i.ApplicationUser)
                .WithOne(i => i.CoordinatorAssistant)
                .HasForeignKey<CoordinatorAssistant>(i => i.ApplicationUserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(i => i.StudentBatch)
                .WithMany(i => i.CoordinatorAssistants)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
