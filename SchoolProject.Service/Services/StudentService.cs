using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Data.Helpers;
using SchoolProject.Infrastructure.Interfaces;
using SchoolProject.Service.Interfaces;

namespace SchoolProject.Service.Services
{
    public class StudentService : IStudentService
    {
        #region Fields
        private readonly IStudentRepository _studentRepository;
        #endregion
        #region Constructors
        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<string> AddAsync(Student student)
        {
            var result = _studentRepository.GetTableNoTracking().Where(s => s.NameEn.Equals(student.NameEn)).FirstOrDefault();
            if (result is not null)
                return "Exist";

            await _studentRepository.AddAsync(student);
            return "Success";
        }

        public async Task<string> DeleteAsync(Student student)
        {
            var trans = _studentRepository.BeginTransaction();
            try
            {
                await _studentRepository.DeleteAsync(student);
                trans.Commit();
                return "Success";
            }
            catch
            {
                _studentRepository.RollBack();
                return "Failed";
            }
        }

        public async Task<string> EditAsync(Student student)
        {
            await _studentRepository.UpdateAsync(student);
            return "Success";
        }

        public IQueryable<Student> FilterStudentPaginatedQueryable(StudentOrderingEnum orderBy, string search)
        {
            var query = _studentRepository.GetTableNoTracking().Include(s => s.Department).AsQueryable();
            if (search is not null)
                query = query.Where(s => s.NameEn.Contains(search));
            switch (orderBy)
            {
                case StudentOrderingEnum.StudId:
                    query = query.OrderBy(s => s.StudID);
                    break;
                case StudentOrderingEnum.Name:
                    query = query.OrderBy(s => s.NameEn);
                    break;
                case StudentOrderingEnum.Address:
                    query = query.OrderBy(s => s.Address);
                    break;
                case StudentOrderingEnum.DepartmentName:
                    query = query.OrderBy(s => s.Department.DNameEn);
                    break;
            }
            return query;

        }

        public async Task<Student> GetByIdAsync(int id)
        {
            var student = await _studentRepository.GetTableNoTracking()
                                                  .Include(s => s.Department)
                                                  .FirstOrDefaultAsync(s => s.StudID == id);
            return student;
        }

        public async Task<Student> GetStudentByIdWithIncludeAsync(int id)
        {
            var student = _studentRepository.GetTableNoTracking()
                                            .Include(s => s.Department)
                                            .Where(s => s.StudID == id)
                                            .FirstOrDefault();
            return student;
        }

        public IQueryable<Student> GetStudentsByDepartmentId(int departmentId)
        {
            return _studentRepository.GetTableNoTracking()
                                     .Where(s => s.DID.Equals(departmentId))
                                     .AsQueryable();
        }
        #endregion
        #region Handle Function
        public async Task<List<Student>> GetStudentsListAsync()
        {
            return await _studentRepository.GetStudentsListAsync();
        }

        public IQueryable<Student> GetStudentsQueryable()
        {
            return _studentRepository.GetTableNoTracking().Include(s => s.Department).AsQueryable();
        }

        public async Task<bool> IsNameExist(string name)
        {
            return await _studentRepository.GetTableNoTracking().AnyAsync(s => s.NameEn!.Equals(name));
        }

        public async Task<bool> IsNameExistExcludeSelf(string name, int id)
        {
            return await _studentRepository.GetTableNoTracking().AnyAsync(s => s.NameEn.Equals(name) && !s.StudID.Equals(id));
        }
        #endregion
    }
}
