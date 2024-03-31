using CareerMate.Models.Entities.InternshipInvites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CareerMate.Infrastructure.Persistence.EntityConfigurations
{
    public class InternshipInviteConfigurations : IEntityTypeConfiguration<InternshipInvite>
    {
        public void Configure(EntityTypeBuilder<InternshipInvite> builder)
        {
            builder.ToTable(nameof(InternshipInvite));

            builder.HasKey(i => i.Id);

            builder.HasOne(i => i.Student)
                .WithMany(i => i.InternshipInvites)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            builder.HasOne(i => i.Internship)
                .WithMany(i => i.InternshipInvites)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
        }
    }
}
