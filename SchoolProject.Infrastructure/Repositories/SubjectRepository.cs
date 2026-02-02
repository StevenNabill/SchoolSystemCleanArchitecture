using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Infrastructure.Context;
using SchoolProject.Infrastructure.Generics;
using SchoolProject.Infrastructure.Interfaces;

namespace SchoolProject.Infrastructure.Repositories
{
    public class SubjectRepository : GenericRepository<Subject>, ISubjectRepository
    {
        #region Fields
        private readonly DbSet<Subject> _subjects;
        #endregion

        #region Constructors
        public SubjectRepository(ApplicationDBContext dbContext)
            : base(dbContext)
        {
            _subjects = dbContext.Set<Subject>();
        }
        #endregion

        #region Functions
        #endregion
    }
}
