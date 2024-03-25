using CareerMate.Models.Entities.InternshipPosts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CareerMate.Infrastructure.Persistence.EntityConfigurations
{
    public class InternshipPostConfigurations : IEntityTypeConfiguration<InternshipPost>
    {
        public void Configure(EntityTypeBuilder<InternshipPost> builder)
        {
            builder.ToTable(nameof(InternshipPost));

            builder.HasKey(i => i.Id);

            builder.HasOne(i => i.Internship)
                .WithOne(i => i.InternshipPost)
                .HasForeignKey<InternshipPost>(i => i.InternshipId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
        }
    }
}
