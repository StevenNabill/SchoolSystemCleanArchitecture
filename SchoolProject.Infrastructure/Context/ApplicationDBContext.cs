using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using System.Reflection;

namespace SchoolProject.Infrastructure.Context
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
            : base(options)
        {

        }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<DepartmentSubject> DepartmentsSubjects { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<StudentSubject> StudentsSubjects { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<InstructorSubject> InstructorsSubjects { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
