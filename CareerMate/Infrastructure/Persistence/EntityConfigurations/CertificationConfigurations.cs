using CareerMate.Models.Entities.Certifications;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CareerMate.Infrastructure.Persistence.EntityConfigurations
{
    public class CertificationConfigurations : IEntityTypeConfiguration<Certification>
    {
        public void Configure(EntityTypeBuilder<Certification> builder)
        {
            builder.ToTable(nameof(Certification));

            builder.HasKey(i => i.Id);

            builder.HasOne(i => i.Student)
                .WithMany(i => i.Certification)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
