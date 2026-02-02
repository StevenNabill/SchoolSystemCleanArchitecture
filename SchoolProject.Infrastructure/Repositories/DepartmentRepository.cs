using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Infrastructure.Context;
using SchoolProject.Infrastructure.Generics;
using SchoolProject.Infrastructure.Interfaces;

namespace SchoolProject.Infrastructure.Repositories
{
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
    {
        #region Fields
        private readonly DbSet<Department> _departments;
        #endregion

        #region Constructors
        public DepartmentRepository(ApplicationDBContext dbContext)
            : base(dbContext)
        {
            _departments = dbContext.Set<Department>();
        }
        #endregion

        #region Functions
        #endregion
    }
}
