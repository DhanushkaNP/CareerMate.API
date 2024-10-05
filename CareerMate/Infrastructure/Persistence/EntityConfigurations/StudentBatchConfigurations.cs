using CareerMate.Models.Entities.StudentBatches;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CareerMate.Infrastructure.Persistence.EntityConfigurations
{
    public class StudentBatchConfigurations : IEntityTypeConfiguration<StudentBatch>
    {
        public void Configure(EntityTypeBuilder<StudentBatch> builder)
        {
            builder.ToTable(nameof(StudentBatch));

            builder.Property(i => i.BatchStartAt).IsRequired();
            
            builder.Property(i => i.BatchEndAt).IsRequired();
            
            builder.Property(i => i.BatchCode).IsRequired();
            
            builder.Property(i => i.LastAllowedDateForStartInternship).IsRequired();

            builder.HasKey(i => i.Id);

            builder.Property(i => i.Id).ValueGeneratedNever();

            builder.HasOne(i => i.Faculty)
                .WithMany(i => i.StudentBatches)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
        }
    }
}
