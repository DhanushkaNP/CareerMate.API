using CareerMate.Models.Entities.Applicants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CareerMate.Infrastructure.Persistence.EntityConfigurations
{
    public class ApplicantConfigurations : IEntityTypeConfiguration<Applicant>
    {
        public void Configure(EntityTypeBuilder<Applicant> builder)
        {
            builder.ToTable(nameof(Applicant));

            builder.HasKey(i => i.Id);

            builder.HasOne(i => i.InternshipPost)
                .WithMany(i => i.Applicants)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            builder.HasOne(i => i.Student)
                .WithMany(i => i.Applicants)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
        }
    }
}
