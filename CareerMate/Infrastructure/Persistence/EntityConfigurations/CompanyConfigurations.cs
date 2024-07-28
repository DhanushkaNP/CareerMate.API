using CareerMate.Models.Entities.Companies;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CareerMate.Infrastructure.Persistence.EntityConfigurations
{
    public class CompanyConfigurations : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.ToTable(nameof(Company));

            builder.HasKey(i => i.Id);

            builder.HasOne(i => i.Faculty)
                .WithMany(i => i.Companies)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            builder.HasOne(i => i.ApplicationUser)
                .WithOne(i => i.Company)
                .HasForeignKey<Company>(i => i.ApplicationUserId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            builder.HasOne(i => i.Industry)
                .WithMany(i => i.Companies)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            builder.OwnsOne(i => i.Ratings, cf =>
            {
                cf.Property(i => i.TotalRatings).HasColumnName("TotalRatings");
                cf.Property(i => i.TotalRaters).HasColumnName("TotalRaters");
            });

            builder.Property(i => i.FoundedOn).HasDefaultValue(null);

            builder.Property(i => i.CompanySize).HasDefaultValue(null);
        }
    }
}
