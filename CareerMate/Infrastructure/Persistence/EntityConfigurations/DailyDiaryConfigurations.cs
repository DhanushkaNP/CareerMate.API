using CareerMate.Abstractions.Enums;
using CareerMate.Models.Entities.DailyDiaries;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CareerMate.Infrastructure.Persistence.EntityConfigurations
{
    public class DailyDiaryConfigurations : IEntityTypeConfiguration<DailyDiary>
    {
        public void Configure(EntityTypeBuilder<DailyDiary> builder)
        {
            builder.ToTable(nameof(DailyDiary));

            builder.HasKey(i => i.Id);

            builder.OwnsOne(i => i.PeriodCovered, cf =>
            {
                cf.Property(i => i.From).HasColumnName("PeriodCoveredFrom");
                cf.Property(i => i.To).HasColumnName("PeriodCoveredTo");
            });

            builder.Navigation(i => i.PeriodCovered).IsRequired();

            builder.OwnsOne(i => i.InternshipPeriod, cf =>
            {
                cf.Property(i => i.From).HasColumnName("InternshipPeriodFrom");
                cf.Property(i => i.To).HasColumnName("InternshipPeriodTo");
            });

            builder.Navigation(i => i.InternshipPeriod).IsRequired();

            builder.OwnsOne(i => i.CoordinatorApproval, cf =>
            {
                cf.Property(i => i.Status).HasColumnName("CoordinatorApprovalStatus").HasDefaultValue(ApprovalTypes.waiting).IsRequired();
                cf.Property(i => i.RequestedApprovalAt).HasColumnName("RequestedCoordinatorApprovalAt");
            });

            builder.Navigation(i => i.CoordinatorApproval).IsRequired();

            builder.OwnsOne(i => i.SupervisorApproval, cf =>
            {
                cf.Property(i => i.Status).HasColumnName("SupervisorApprovalStatus").HasDefaultValue(ApprovalTypes.waiting).IsRequired();
                cf.Property(i => i.RequestedApprovalAt).HasColumnName("RequestedSupervisorApprovalAt");
            });

            builder.Navigation(i => i.CoordinatorApproval).IsRequired();

            builder.HasOne(i => i.Intern)
                .WithMany(i => i.Diary)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
