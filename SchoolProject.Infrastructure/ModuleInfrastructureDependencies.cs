using Microsoft.Extensions.DependencyInjection;
using SchoolProject.Infrastructure.Generics;
using SchoolProject.Infrastructure.Interfaces;
using SchoolProject.Infrastructure.Repositories;

namespace SchoolProject.Infrastructure
{
    public static class ModuleInfrastructureDependencies
    {
        public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services)
        {
            services.AddTransient<IStudentRepository, StudentRepository>();
            services.AddTransient<IDepartmentRepository, DepartmentRepository>();
            services.AddTransient<IInstructorRepository, InstructorRepository>();
            services.AddTransient<ISubjectRepository, SubjectRepository>();
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            return services;
        }
    }
}
