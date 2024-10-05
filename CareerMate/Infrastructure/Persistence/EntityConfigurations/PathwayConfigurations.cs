using CareerMate.Models.Entities.Pathways;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CareerMate.Infrastructure.Persistence.EntityConfigurations
{
    public class PathwayConfigurations : IEntityTypeConfiguration<Pathway>
    {
        public void Configure(EntityTypeBuilder<Pathway> builder)
        {
            builder.ToTable(nameof(Pathway));

            builder.HasKey(i => i.Id);

            builder.HasOne(i => i.Degree)
                .WithMany(i => i.Pathways)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
        }
    }
}
