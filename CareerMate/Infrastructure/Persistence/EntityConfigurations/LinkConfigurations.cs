using CareerMate.Models.Entities.Links;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CareerMate.Infrastructure.Persistence.EntityConfigurations
{
    public class LinkConfigurations : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> builder)
        {
            builder.ToTable(nameof(Contact));

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
