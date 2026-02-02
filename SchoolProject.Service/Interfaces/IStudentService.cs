using SchoolProject.Data.Entities;
using SchoolProject.Data.Helpers;

namespace SchoolProject.Service.Interfaces
{
    public interface IStudentService
    {
        Task<List<Student>> GetStudentsListAsync();
        Task<Student> GetStudentByIdWithIncludeAsync(int id);
        Task<Student> GetByIdAsync(int id);
        Task<string> AddAsync(Student student);
        Task<bool> IsNameExist(string name);
        Task<bool> IsNameExistExcludeSelf(string name, int id);
        Task<string> EditAsync(Student student);
        Task<string> DeleteAsync(Student student);
        IQueryable<Student> GetStudentsQueryable();
        IQueryable<Student> GetStudentsByDepartmentId(int departmentId);
        IQueryable<Student> FilterStudentPaginatedQueryable(StudentOrderingEnum orderBy, string search);

    }
}
