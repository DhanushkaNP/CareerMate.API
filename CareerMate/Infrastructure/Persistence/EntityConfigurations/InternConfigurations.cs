using CareerMate.Models.Entities.Interns;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CareerMate.Infrastructure.Persistence.EntityConfigurations
{
    public class InternConfigurations : IEntityTypeConfiguration<Intern>
    {
        public void Configure(EntityTypeBuilder<Intern> builder)
        {
            builder.ToTable(nameof(Intern));

            builder.HasKey(i => i.Id);

            builder.HasOne(i => i.Student)
                .WithOne(i => i.Intern)
                .HasForeignKey<Intern>(i => i.StudentId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            builder.HasOne(i => i.Internship)
                .WithMany(i => i.Interns)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            builder.HasOne(i => i.Supervisor)
                .WithMany(i => i.Interns)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(i => i.Company)
                .WithMany(i => i.Interns)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
