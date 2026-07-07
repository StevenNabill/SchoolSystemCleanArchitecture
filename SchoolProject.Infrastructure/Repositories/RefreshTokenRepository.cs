using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Infrastructure.Context;
using SchoolProject.Infrastructure.Generics;
using SchoolProject.Infrastructure.Interfaces;

namespace SchoolProject.Infrastructure.Repositories
{
    public class RefreshTokenRepository : GenericRepository<UserRefreshToken>, IRefreshTokenRepository
    {
        #region Fields
        private readonly DbSet<UserRefreshToken> userRefreshTokens;
        #endregion

        #region Constructors
        public RefreshTokenRepository(ApplicationDBContext dbContext)
            : base(dbContext)
        {
            userRefreshTokens = dbContext.Set<UserRefreshToken>();
        }
        #endregion

    }
}
