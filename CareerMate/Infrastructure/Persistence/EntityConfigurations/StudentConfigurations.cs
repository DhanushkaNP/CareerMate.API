using CareerMate.Models.Entities.Students;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CareerMate.Infrastructure.Persistence.EntityConfigurations
{
    public class StudentConfigurations : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.ToTable(nameof(Student));

            builder.HasKey(i => i.Id);

            builder.Property(i => i.StudentId).IsRequired();
            
            builder.Property(i => i.FirstName);
            
            builder.Property(i => i.LastName);
            
            builder.Property(i => i.UniversityEmail).IsRequired();
            
            builder.Property(i => i.PhoneNumber);
            
            builder.Property(i => i.CGPA);
            
            builder.Property(i => i.PersonalEmail);

            builder.HasOne(i => i.Batch)
                .WithMany(i => i.Students)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            builder.HasOne(i => i.ApplicationUser)
                .WithOne(i => i.Student)
                .HasForeignKey<Student>(i => i.ApplicationUserId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired(false);

            builder.HasOne(i => i.Degree)
                .WithMany(i => i.Students)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(i => i.Pathway)
                .WithMany(i => i.Students)
                .OnDelete(DeleteBehavior.Cascade);

            builder.OwnsOne(i => i.CompanyFeedback, cf =>
            {
                cf.Property(i => i.Level).HasColumnName("CompanyFeedbackLevel").IsRequired(false);
                cf.Property(i => i.Message).HasColumnName("CompanyFeedbackMessage").IsRequired(false);
            });

            builder.Navigation(i => i.CompanyFeedback).IsRequired();

            builder.OwnsOne(i => i.Marks, m =>
            {
                m.Property(i => i.DailyDiary).HasColumnName("DailyDiaryMarks");
                m.Property(i => i.Viva).HasColumnName("VivaMarks");
                m.Property(i => i.Others).HasColumnName("OthersMarks");
                m.Property(i => i.Total).HasColumnName("TotalMarks");
            });

            builder.Navigation(i => i.Marks).IsRequired();
        }
    }
}
