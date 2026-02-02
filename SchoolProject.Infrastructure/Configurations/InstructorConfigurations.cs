using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolProject.Data.Entities;

namespace SchoolProject.Infrastructure.Configurations
{
    public class InstructorConfigurations : IEntityTypeConfiguration<Instructor>
    {
        public void Configure(EntityTypeBuilder<Instructor> builder)
        {
            builder.HasKey(i => i.InsId);

            builder
            .HasOne(i => i.SuperVisor)
            .WithMany(i => i.SuperVisedInstructors)
            .HasForeignKey(i => i.SuperVisorId)
            .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
