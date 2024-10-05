using CareerMate.Models.Entities.CompanyFollowers;
using CareerMate.Models.Entities.Students;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CareerMate.Infrastructure.Persistence.EntityConfigurations
{
    public class CompanyFollowerConfigurations : IEntityTypeConfiguration<CompanyFollower>
    {
        public void Configure(EntityTypeBuilder<CompanyFollower> builder)
        {
            builder.ToTable(nameof(CompanyFollower));

            builder.HasKey(i => i.Id);

            builder.HasOne(i => i.Student)
                .WithMany(i => i.CompanyFollowers)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            builder.HasOne(i => i.Company)
                .WithMany(i => i.Followers)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
        }
    }
}
