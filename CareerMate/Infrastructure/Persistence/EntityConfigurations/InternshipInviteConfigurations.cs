using CareerMate.Models.Entities.InternshipInvites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CareerMate.Infrastructure.Persistence.EntityConfigurations
{
    public class InternshipInviteConfigurations : IEntityTypeConfiguration<InternshipOffer>
    {
        public void Configure(EntityTypeBuilder<InternshipOffer> builder)
        {
            builder.ToTable(nameof(InternshipOffer));

            builder.HasKey(i => i.Id);

            builder.HasOne(i => i.Student)
                .WithMany(i => i.InternshipOffers)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            builder.HasOne(i => i.Internship)
                .WithMany(i => i.InternshipOffers)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            builder.HasOne(i => i.Supervisor)
                .WithMany(i => i.InternshipOffers)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
        }
    }
}
