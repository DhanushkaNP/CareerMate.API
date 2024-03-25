using CareerMate.Models.Entities.Internships;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CareerMate.Infrastructure.Persistence.EntityConfigurations
{
    public class InternshipConfigurations : IEntityTypeConfiguration<Internship>
    {
        public void Configure(EntityTypeBuilder<Internship> builder)
        {
            builder.ToTable(nameof(Internship));

            builder.HasKey(i => i.Id);

            builder.HasOne(i => i.Company)
                .WithMany(i => i.Internships)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
        }
    }
}
