using CareerMate.Models.Entities.Skills;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CareerMate.Infrastructure.Persistence.EntityConfigurations
{
    public class SkillConfigurations : IEntityTypeConfiguration<Skill>
    {
        public void Configure(EntityTypeBuilder<Skill> builder)
        {
            builder.ToTable(nameof(Skill));

            builder.HasKey(i => i.Id);

            builder.HasOne(i => i.Student)
                .WithMany(i => i.Skills)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(i => i.Company)
                .WithMany(i => i.SkillsLookingFor)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
