using CareerMate.Models.Entities.Supervisors;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CareerMate.Infrastructure.Persistence.EntityConfigurations
{
    public class SupervisorConfigurations : IEntityTypeConfiguration<Supervisor>
    {
        public void Configure(EntityTypeBuilder<Supervisor> builder)
        {
            builder.ToTable(nameof(Supervisor));

            builder.HasKey(i => i.Id);

            builder.HasOne(i => i.ApplicationUser)
                .WithOne(i => i.Supervisor)
                .HasForeignKey<Supervisor>(i => i.ApplicationUserId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            builder.HasOne(i => i.Company)
                .WithMany(i => i.Supervisors)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            builder.HasOne(i => i.Company)
                .WithMany(i => i.Supervisors)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
        }
    }
}
