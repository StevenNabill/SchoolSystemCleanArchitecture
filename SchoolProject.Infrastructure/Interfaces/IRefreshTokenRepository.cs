using SchoolProject.Data.Entities.Identity;
using SchoolProject.Infrastructure.Generics;

namespace SchoolProject.Infrastructure.Interfaces
{
    public interface IRefreshTokenRepository : IGenericRepository<UserRefreshToken>
    {
    }
}
