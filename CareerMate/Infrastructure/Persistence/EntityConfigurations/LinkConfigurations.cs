using CareerMate.Models.Entities.Links;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CareerMate.Infrastructure.Persistence.EntityConfigurations
{
    public class LinkConfigurations : IEntityTypeConfiguration<Link>
    {
        public void Configure(EntityTypeBuilder<Link> builder)
        {
            builder.ToTable(nameof(Link));

            builder.HasKey(i => i.Id);

            builder.HasOne(i => i.Student)
               .WithMany(i => i.Links)
               .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(i => i.Company)
               .WithMany(i => i.Links)
               .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
