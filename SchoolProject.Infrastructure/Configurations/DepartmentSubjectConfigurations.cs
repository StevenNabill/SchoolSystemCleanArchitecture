using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolProject.Data.Entities;

namespace SchoolProject.Infrastructure.Configurations
{
    public class DepartmentSubjectConfigurations : IEntityTypeConfiguration<DepartmentSubject>
    {
        public void Configure(EntityTypeBuilder<DepartmentSubject> builder)
        {
            builder.HasKey(x => new { x.DID, x.SubID });

            builder
            .HasOne(ds => ds.Department)
            .WithMany(d => d.DepartmentSubjects)
            .HasForeignKey(ds => ds.DID);

            builder
            .HasOne(e => e.Subject)
            .WithMany(s => s.DepartmentsSubjects)
            .HasForeignKey(e => e.SubID);
        }
    }
}
