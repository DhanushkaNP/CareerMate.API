using CareerMate.Models.Entities.DailyRecords;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CareerMate.Infrastructure.Persistence.EntityConfigurations
{
    public class DailyRecordConfigurations : IEntityTypeConfiguration<DailyRecord>
    {
        public void Configure(EntityTypeBuilder<DailyRecord> builder)
        {
            builder.ToTable(nameof(DailyRecord));

            builder.HasKey(i => i.Id);

            builder.HasOne(i => i.Diary)
                .WithMany(i => i.Records)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
        }
    }
}
