using CareerMate.Models.Entities.Universities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CareerMate.Infrastructure.Persistence.EntityConfigurations
{
    public class UniversityConfigurations : IEntityTypeConfiguration<University>
    {
        public void Configure(EntityTypeBuilder<University> builder)
        {
            builder.ToTable(nameof(University));
            
            builder.Property(i => i.Name).IsRequired();
            
            builder.Property(i => i.ShortName).IsRequired();

            builder.HasKey(i => i.Id);

            builder.Property(i => i.Id).ValueGeneratedNever();
        }
    }
}
