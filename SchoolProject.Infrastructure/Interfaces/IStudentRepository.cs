using SchoolProject.Data.Entities;
using SchoolProject.Infrastructure.Generics;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolProject.Infrastructure.Interfaces
{
    public interface IStudentRepository : IGenericRepository<Student>
    {
        Task<List<Student>> GetStudentsListAsync();
    }
}
