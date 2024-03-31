using CareerMate.Models.Entities.Faculties;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CareerMate.Infrastructure.Persistence.EntityConfigurations
{
    public class FacultyConfigurations : IEntityTypeConfiguration<Faculty>
    {
        public void Configure(EntityTypeBuilder<Faculty> builder)
        {
            builder.ToTable(nameof(Faculty));

            builder.HasKey(i => i.Id);

            builder.Property(i => i.Name).IsRequired();
            
            builder.Property(i => i.ShortName).IsRequired();

            builder.Property(i => i.Id).ValueGeneratedNever();

            builder.HasOne(i => i.University)
                .WithMany(i => i.Faculty)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            builder.HasOne(i => i.University)
               .WithMany(i => i.Faculty)
               .OnDelete(DeleteBehavior.Cascade)
               .IsRequired();
        }
    }
}
