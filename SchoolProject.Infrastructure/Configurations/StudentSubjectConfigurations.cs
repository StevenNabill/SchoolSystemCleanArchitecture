using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolProject.Data.Entities;

namespace SchoolProject.Infrastructure.Configurations
{
    public class StudentSubjectConfigurations : IEntityTypeConfiguration<StudentSubject>
    {
        public void Configure(EntityTypeBuilder<StudentSubject> builder)
        {
            builder.HasKey(x => new { x.StudID, x.SubID });

            builder
            .HasOne(ss => ss.Student)
            .WithMany(s => s.StudentsSubjects)
            .HasForeignKey(ss => ss.StudID);

            builder
            .HasOne(ss => ss.Subject)
            .WithMany(s => s.StudentsSubjects)
            .HasForeignKey(ss => ss.SubID);
        }
    }
}
