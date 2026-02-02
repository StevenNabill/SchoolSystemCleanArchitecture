using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolProject.Data.Entities;

namespace SchoolProject.Infrastructure.Configurations
{
    public class InstructorSubjectConfigurations : IEntityTypeConfiguration<InstructorSubject>
    {
        public void Configure(EntityTypeBuilder<InstructorSubject> builder)
        {
            builder.HasKey(x => new { x.InsId, x.SubId });

            builder
            .HasOne(insSub => insSub.Instructor)
            .WithMany(ins => ins.InstructorsSubjects)
            .HasForeignKey(insSub => insSub.InsId);

            builder
            .HasOne(insSub => insSub.Subject)
            .WithMany(ins => ins.InstructorsSubjects)
            .HasForeignKey(insSub => insSub.SubId);
        }
    }
}
