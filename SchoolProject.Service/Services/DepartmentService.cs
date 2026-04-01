using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Infrastructure.Interfaces;
using SchoolProject.Service.Interfaces;

namespace SchoolProject.Service.Services
{
    public class DepartmentService : IDepartmentService
    {
        #region Fields
        private readonly IDepartmentRepository _departmentRepository;
        #endregion

        #region Constructor
        public DepartmentService(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }
        #endregion

        #region Functions
        public async Task<Department> GetDepartmentByIdWithIncludeAsync(int id)
        {
            var department = await _departmentRepository.GetTableNoTracking()
                                 .Where(d => d.DID.Equals(id))
                                 .Include(d => d.DepartmentSubjects).ThenInclude(ds => ds.Subject)
                                 .Include(d => d.DepartmentManager)
                                 .Include(d => d.Instructors)
                                 .FirstOrDefaultAsync();
            return department;
        }

        public async Task<bool> IsDepartmentExist(int id)
        {
            return await _departmentRepository
                .GetTableNoTracking()
                .AnyAsync(d => d.DID == id);
        }
        #endregion

    }
}
