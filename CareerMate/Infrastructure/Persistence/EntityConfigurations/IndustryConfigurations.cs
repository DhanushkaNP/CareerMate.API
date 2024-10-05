using CareerMate.Models.Entities.Industries;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CareerMate.Infrastructure.Persistence.EntityConfigurations
{
    public class IndustryConfigurations : IEntityTypeConfiguration<Industry>
    {
        public void Configure(EntityTypeBuilder<Industry> builder)
        {
            builder.ToTable(nameof(Industry));

            builder.HasKey(i => i.Id);

            builder.HasOne(i => i.Faculty)
                .WithMany(i => i.Industries)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
