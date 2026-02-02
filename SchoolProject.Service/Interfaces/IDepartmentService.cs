using SchoolProject.Data.Entities;

namespace SchoolProject.Service.Interfaces
{
    public interface IDepartmentService
    {
        Task<Department> GetDepartmentByIdWithIncludeAsync(int id);
    }
}
