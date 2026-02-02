using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolProject.Data.Entities;

namespace SchoolProject.Infrastructure.Configurations
{
    public class DepartmentConfigurations : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.HasKey(d => d.DID);

            builder
            .HasMany(d => d.Students)
            .WithOne(s => s.Department)
            .HasForeignKey(s => s.DID)
            .OnDelete(DeleteBehavior.Restrict);

            builder
            .HasOne(d => d.DepartmentManager)
            .WithOne(i => i.ManagedDepartment)
            .HasForeignKey<Department>(d => d.InsManagerId)
            .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
