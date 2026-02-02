using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Infrastructure.Context;
using SchoolProject.Infrastructure.Generics;
using SchoolProject.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolProject.Infrastructure.Repositories
{
    public class StudentRepository : GenericRepository<Student>, IStudentRepository
    {
        #region Fields
        private readonly DbSet<Student> _students;
        #endregion

        #region Constructors
        public StudentRepository(ApplicationDBContext dbContext)
            : base(dbContext)
        {
            _students = dbContext.Set<Student>();
        }
        #endregion

        #region Handle Functions
        public async Task<List<Student>> GetStudentsListAsync()
        {
            return await _students.Include(s => s.Department).ToListAsync();
        }
        #endregion
    }
}
