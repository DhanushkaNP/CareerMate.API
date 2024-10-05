using CareerMate.Models.Entities.CompanyLeaveRequests;
using CareerMate.Models.Entities.Students;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CareerMate.Infrastructure.Persistence.EntityConfigurations
{
    public class CompanyLeaveRequestConfigurations : IEntityTypeConfiguration<CompanyLeaveRequest>
    {
        public void Configure(EntityTypeBuilder<CompanyLeaveRequest> builder)
        {
            builder.ToTable(nameof(CompanyLeaveRequest));

            builder.Property(i => i.Approved);

            builder.HasKey(i => i.Id);

            builder.HasOne(i => i.Student)
                .WithMany(i => i.LeaveRequests)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
